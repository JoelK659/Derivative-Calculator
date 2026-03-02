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

        //public override Expression Simplify()
        //{

        //}
    }
}
