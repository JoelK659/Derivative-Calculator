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
            return $"({Left} * {Right})";
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
    }
}
