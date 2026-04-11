using System;
using System.Collections.Generic;
using System.Text;

namespace Derivative_and_Integral_Calculator.Expressions
{
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

        public override string Explain(string exp)
        {
            return $"{Value}: The derivative of a constant is always 0, so the derivative of {Value} is 0." + Environment.NewLine;
        }
    }
}
