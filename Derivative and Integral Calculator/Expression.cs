using System;
using System.Collections.Generic;
using System.Text;

namespace Derivative_and_Integral_Calculator
{
    abstract class Expression
    {
        //Each expression should know how to differentiate itself
        public abstract Expression Differentiate();
        //Each expression should know how to represent itself as a string
        public abstract override string ToString();

        public abstract Expression Simplify();
    }

    class ConstantExpression : Expression
    {
        public double Value;

        public ConstantExpression(double value)
        {
            Value = value;
        }
        public override Expression Differentiate()
        {
            //The derivative of a constant is zero
            return new ConstantExpression(0);
        }
        public override string ToString()
        {
            return Value.ToString();
        }

        public override Expression Simplify()
        {
            return this;
        }
    }

    class VariableExpression : Expression
    {
        public override Expression Differentiate()
        {
            //The derivative of x with respect to x is 1
            return new ConstantExpression(1);
        }

        public override string ToString()
        {
            return "x";
        }

        public override Expression Simplify()
        {
            return this;
        }
    }

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
            //Sum Rule: (f + g)' = f' + g'
            return new AddExpression(Left.Differentiate(), Right.Differentiate());
        }

        public override string ToString()
        {
            return $"({Left} + {Right})";

        }

        public override Expression Simplify()
        {
            //Simplify both sides first
            var left = Left.Simplify();
            var right = Right.Simplify();

            //Collect terms for simplification
            var terms = new List<Expression>();
            CollectTerms(left, terms);
            CollectTerms(right, terms);

            //Combine like terms: constants and x terms
            double constantSum = 0;
            double xCoefficient = 0;
            var otherTerms = new List<Expression>();

            //Go through collected terms and combine constants and x terms
            foreach (var term in terms)
            {
                switch (term)
                {
                    case ConstantExpression c:
                        constantSum += c.Value;
                        break;

                    case VariableExpression:
                        xCoefficient += 1;
                        break;

                    case ProductExpression p
                        when p.Left is ConstantExpression coeff &&
                             p.Right is VariableExpression:
                        xCoefficient += coeff.Value;
                        break;

                    default:
                        otherTerms.Add(term);
                        break;
                }
            }

            //Construct the simplified expression from the combined terms
            var resultTerms = new List<Expression>();

            //Add x term if it exists
            if (xCoefficient != 0)
            {
                if (xCoefficient == 1)
                    resultTerms.Add(new VariableExpression());
                else
                    resultTerms.Add(
                        new ProductExpression(
                            new ConstantExpression(xCoefficient),
                            new VariableExpression()
                        )
                    );
            }

            //Add constant term if it exists
            if (constantSum != 0)
                resultTerms.Add(new ConstantExpression(constantSum));

            //Add any other non-combinable terms
            resultTerms.AddRange(otherTerms);

            //If we have no terms left, return zero
            if (resultTerms.Count == 0)
                return new ConstantExpression(0);

            //If we have only one term, return it directly
            Expression result = resultTerms[0];
            for (int i = 1; i < resultTerms.Count; i++)
            {
                result = new AddExpression(result, resultTerms[i]);
            }

            return result;
        }

        //Helper method to collect terms for simplification
        private void CollectTerms(Expression expression, List<Expression> terms)
        {
            if (expression is AddExpression add)
            {
                CollectTerms(add.Left, terms);
                CollectTerms(add.Right, terms);
            }
            else
            {
                terms.Add(expression);
            }
        }
    }

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
            //Product Rule: (f * g)' = (f' * g) + (f * g')
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
            //Simplify both sides
            var simplifiedLeft = Left.Simplify();
            var simplifiedRight = Right.Simplify();
            //If either side is zero, the whole product is zero
            if (simplifiedLeft is ConstantExpression leftConst && leftConst.Value == 0 ||
                simplifiedRight is ConstantExpression rightConst && rightConst.Value == 0)
            {
                return new ConstantExpression(0);
            }
            //If one side is one, return the other side
            if (simplifiedLeft is ConstantExpression leftOne && leftOne.Value == 1)
            {
                return simplifiedRight;
            }
            if (simplifiedRight is ConstantExpression rightOne && rightOne.Value == 1)
            {
                return simplifiedLeft;
            }

            //If both sides are constants, we can multiply them
            if (simplifiedLeft is ConstantExpression leftConst2 && simplifiedRight is ConstantExpression rightConst2)
            {
                return new ConstantExpression(leftConst2.Value * rightConst2.Value);
            }

            //If we have a constant multiplied by a product, we can combine the constants => (2 * (3 * x)) => (6 * x)
            if (simplifiedLeft is ConstantExpression outerConst &&
                simplifiedRight is ProductExpression innerProd &&
                innerProd.Left is ConstantExpression innerConst)
            {
                return new ProductExpression(
                    new ConstantExpression(outerConst.Value * innerConst.Value),
                    innerProd.Right).Simplify();
            }

            //If we have a constant multiplied by a product, we can combine the constants => ((3 * x) * 2) => (6 * x)
            if (simplifiedRight is ConstantExpression outerConst2 &&
                simplifiedLeft is ProductExpression innerProd2 &&
                innerProd2.Left is ConstantExpression innerConst2)
            {
                return new ProductExpression(
                    new ConstantExpression(outerConst2.Value * innerConst2.Value),
                    innerProd2.Right).Simplify();
            }
            //Otherwise, return a new ProductExpression with the simplified parts
            return new ProductExpression(simplifiedLeft, simplifiedRight);
        }
    }

    class PowerExpression : Expression
    {
        public Expression Base;
        public int Exponent;

        public PowerExpression(Expression baseExpr, int exponent)
        {
            Base = baseExpr;
            Exponent = exponent;
        }

        public override Expression Differentiate()
        {
            //Power Rule: (f^n)' = n * f^(n-1)

            return new ProductExpression (
                new ConstantExpression(Exponent),
                new PowerExpression(Base, Exponent - 1)
            );
        }

        public override string ToString()
        {
            return $"({Base}^{Exponent})";
        }

        public override Expression Simplify()
        {
            //Simplify the base
            var simplifiedBase = Base.Simplify();
            //If the exponent is zero, the result is 1
            if (Exponent == 0)
            {
                return new ConstantExpression(1);
            }
            //If the exponent is one, return the base
            if (Exponent == 1)
            {
                return simplifiedBase;
            }
            //If the base is a constant, we can compute the power
            if (simplifiedBase is ConstantExpression baseConst)
            {
                return new ConstantExpression(Math.Pow(baseConst.Value, Exponent));
            }
            //Otherwise, return a new PowerExpression with the simplified base
            return new PowerExpression(simplifiedBase, Exponent);
        }
    }
}
