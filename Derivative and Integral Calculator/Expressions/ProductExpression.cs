using System;
using System.Collections.Generic;
using System.Text;

namespace Derivative_and_Integral_Calculator.Expressions
{
    class ProductExpression : Expression
    {
        public Expression Left;
        public Expression Right;

        public ProductExpression(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override Expression Differentiate()
        {
            // Product rule: f'g + fg'
            return new AddExpression(
                new ProductExpression(Left.Differentiate(), Right),
                new ProductExpression(Left, Right.Differentiate())
            );
        }

        public override string ToString()
        {
            return $"({Left}{Right})";
        }

        public override Expression Simplify()
        {
            // Step 1: Simplify left and right recursively
            var left = Left.Simplify();
            var right = Right.Simplify();

            // Step 2: Handle distribution over addition
            if (left is AddExpression la)
            {
                return new AddExpression(
                    new ProductExpression(la.Left, right).Simplify(),
                    new ProductExpression(la.Right, right).Simplify()
                ).Simplify();
            }

            if (right is AddExpression ra)
            {
                return new AddExpression(
                    new ProductExpression(left, ra.Left).Simplify(),
                    new ProductExpression(left, ra.Right).Simplify()
                ).Simplify();
            }

            // Step 3: Flatten nested products
            var factors = FlattenProduct(new ProductExpression(left, right));

            // Step 4: Simplify factors and multiply constants
            double constProduct = 1;
            var simplifiedFactors = new List<Expression>();
            foreach (var f in factors)
            {
                var s = f.Simplify();
                if (s is ConstantExpression c)
                    constProduct *= c.Value;
                else
                    simplifiedFactors.Add(s);
            }

            if (constProduct == 0) return new ConstantExpression(0);
            if (constProduct != 1) simplifiedFactors.Insert(0, new ConstantExpression(constProduct));

            // Step 5: COLLAPSIBLE CHAINED MULTIPLICATION (NEW)
            var factorDict = new Dictionary<string, (Expression baseExpr, int power)>();
            var others = new List<Expression>();

            foreach (var f in simplifiedFactors)
            {
                if (f is VariableExpression v)
                {
                    string key = v.ToString();
                    if (factorDict.ContainsKey(key))
                        factorDict[key] = (v, factorDict[key].power + 1);
                    else
                        factorDict[key] = (v, 1);
                }
                else if (f is PowerExpression p)
                {
                    string key = p.Base.ToString();
                    if (factorDict.ContainsKey(key))
                        factorDict[key] = (p.Base, factorDict[key].power + p.Exponent);
                    else
                        factorDict[key] = (p.Base, p.Exponent);
                }
                else
                {
                    others.Add(f);
                }
            }

            // Step 6: Rebuild product from combined factors
            var finalFactors = new List<Expression>();
            foreach (var kvp in factorDict)
            {
                if (kvp.Value.power == 1)
                    finalFactors.Add(kvp.Value.baseExpr);
                else
                    finalFactors.Add(new PowerExpression(kvp.Value.baseExpr, kvp.Value.power));
            }
            finalFactors.AddRange(others);

            // Step 7: Chain into ProductExpression
            Expression result;
            if (finalFactors.Count == 0)
                result = new ConstantExpression(1);
            else
            {
                result = finalFactors[0];
                for (int i = 1; i < finalFactors.Count; i++)
                    result = new ProductExpression(result, finalFactors[i]);
            }

            return result;
        }

        // Helper to flatten nested products (keep as-is)

        private List<Expression> FlattenProduct(Expression expr)
        {
            var list = new List<Expression>();
            if (expr is ProductExpression p)
            {
                list.AddRange(FlattenProduct(p.Left));
                list.AddRange(FlattenProduct(p.Right));
            }
            else list.Add(expr);
            return list;
        }
    }
}
