using System;
using System.Collections.Generic;
using System.Text;

namespace Derivative_and_Integral_Calculator.Expressions
{
    class SubtractExpression : Expression
    {
        public Expression Left;
        public Expression Right;

        public SubtractExpression(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override Expression Differentiate()
        {
            return new SubtractExpression(Left.Differentiate(), Right.Differentiate());
        }

        public override string ToString()
        {
            return $"({Left} - {Right})";
        }

        //public override Expression Simplify()
        //{

        //}
    }
}
