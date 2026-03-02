using System;
using System.Collections.Generic;
using System.Text;
using Derivative_and_Integral_Calculator.Parsing;

namespace Derivative_and_Integral_Calculator.Expressions
{
    class AddExpression : Expression
    {
        public Expression Left;
        public Expression Right;

        public AddExpression(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override Expression Differentiate()
        {
            return new AddExpression(Left.Differentiate(), Right.Differentiate());
        }

        public override string ToString()
        {
            return $"({Left} + {Right})";
        }

        public override Expression Simplify()
        {
            // Step 0: recursively simplify Left and Right
            var leftSimplified = Left.Simplify();
            var rightSimplified = Right.Simplify();

            // Step 1: flatten nested additions after simplification
            var terms = FlattenAdd(new AddExpression(leftSimplified, rightSimplified));

            // Step 2: combine constant terms
            double constantSum = 0;
            var newTerms = new List<Expression>();
            foreach (var t in terms)
            {
                var s = t.Simplify(); // double-check simplification after flattening
                if (s is ConstantExpression c)
                    constantSum += c.Value;
                else
                    newTerms.Add(s);
            }
            if (constantSum != 0)
                newTerms.Add(new ConstantExpression(constantSum));

            // Step 3: combine like variables / like powers
            newTerms = CombineLikeTerms(newTerms);

            // Step 4: rebuild flattened addition
            if (newTerms.Count == 0) return new ConstantExpression(0);
            Expression result = newTerms[0];
            for (int i = 1; i < newTerms.Count; i++)
                result = new AddExpression(result, newTerms[i]);

            return result;
        }

        private List<Expression> FlattenAdd(Expression expr)
        {
            var list = new List<Expression>();
            if (expr is AddExpression add)
            {
                list.AddRange(FlattenAdd(add.Left));
                list.AddRange(FlattenAdd(add.Right));
            }
            else list.Add(expr);
            return list;
        }

        private List<Expression> CombineLikeTerms(List<Expression> terms)
        {
            var result = new List<Expression>();

            // key → (base expression, coefficient)
            var collected =
                new Dictionary<string, (Expression expr, double coeff)>();

            foreach (var term in terms)
            {
                double coeff = 1;
                Expression baseExpr = term;

                // detect coefficient form: c * expr
                if (term is ProductExpression p &&
                    p.Left is ConstantExpression c)
                {
                    coeff = c.Value;
                    baseExpr = p.Right;
                }

                else if (term is ConstantExpression)
                {
                    result.Add(term);
                    continue;
                }

                string key = baseExpr.ToString();

                if (collected.ContainsKey(key))
                {
                    var old = collected[key];
                    collected[key] =
                        (old.expr, old.coeff + coeff);
                }
                else
                {
                    collected[key] = (baseExpr, coeff);
                }
            }

            // rebuild combined terms
            foreach (var entry in collected.Values)
            {
                if (entry.coeff == 0)
                    continue;

                if (entry.coeff == 1)
                    result.Add(entry.expr);
                else
                    result.Add(
                        new ProductExpression(
                            new ConstantExpression(entry.coeff),
                            entry.expr));
            }

            return result;
        }
    }
}
