using System;
using System.Collections.Generic;
using System.Text;

namespace Derivative_and_Integral_Calculator.Expressions
{
    class VariableExpression : Expression
    {

        public string Name { get; }

        public VariableExpression(string name)
        {
            Name = name;
        }

        public override string ToString() => Name;

        public override Expression Differentiate()
        {
            //The derivative of x with respect to x is 1
            return new ConstantExpression(1);
        }

        public override Expression Simplify()
        {
            return this;
        }

        public override string Explain(string exp)
        {
            return $"{Name}: The derivative of a variable with respect to itself is always 1, so the derivative of {Name} is 1." + Environment.NewLine;
        }

    }
}
