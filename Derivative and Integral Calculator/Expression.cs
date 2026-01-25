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
}
