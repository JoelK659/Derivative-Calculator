using System;
using System.Collections.Generic;
using System.Text;

namespace Derivative_and_Integral_Calculator.Expressions
{
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

            return new ProductExpression(
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
