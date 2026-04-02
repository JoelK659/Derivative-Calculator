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

            //Chain Rule: (f(g(x)))' = f'(g(x)) * g'(x)
            if (Base is ProductExpression || Base is DivideExpression || Base is PowerExpression || Base is AddExpression || Base is SubtractExpression)
            {
                return new ProductExpression(
                    new ProductExpression(
                        new ConstantExpression(Exponent),
                        new PowerExpression(Base, Exponent - 1)
                    ),
                    Base.Differentiate()
                );
            }
            //Simple Power Rule for cases like x^n
            return new ProductExpression(
                new ConstantExpression(Exponent),
                new PowerExpression(Base, Exponent - 1)
            );
        }

        public override string ToString()
        {
            if (Exponent == 1)
            {
                return $"{Base}";
            }
            //return $"{Base}^{Exponent}";
            return $"({Base}^{Exponent})";
        }

        public override Expression Simplify()
        {
            Base = this.Base.Simplify();

            //Zero Rule: x^0 = 1
            if (Exponent == 0)
            {
                if(Base is ConstantExpression baseConst && baseConst.Value == 0)
                {
                    throw new InvalidOperationException("0^0 is undefined.");
                }
                return new ConstantExpression(1);
            }
            //One Rule: x^1 = x
            if (Exponent == 1)
            {
                return Base;
            }

            //Constant Rule: x^n = x^n
            if (Base is ConstantExpression baseConst2)
            {
                return new ConstantExpression(Math.Pow(baseConst2.Value, Exponent));
            }
            //Power Rule: (x^m)^n = x^(m*n)
            if (Base is PowerExpression basePower)
            {
                return new PowerExpression(basePower.Base, basePower.Exponent * Exponent).Simplify();
            }

            return new PowerExpression(Base, Exponent);
        }
    }
}
