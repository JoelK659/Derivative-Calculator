using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
            //return $"{Left} + {Right}";
            return $"({Left} + {Right})";
        }

        public override Expression Simplify()
        {
            Left = Left.Simplify();
            Right = Right.Simplify();

            //Zero Rule: 0 + x = x
            if (Left is ConstantExpression leftConst && leftConst.Value == 0)
            {
                return Right;
            }
            if (Right is ConstantExpression rightConst && rightConst.Value == 0)
            {
                return Left;
            }

            //Constant Rule: 2 + 3 = 5
            if (Left is ConstantExpression leftConst2 && Right is ConstantExpression rightConst2)
            {
                return new ConstantExpression(leftConst2.Value + rightConst2.Value);
            }

            //Negative Constant Collapse: x + (-2) = x - 2
            if (Left is ConstantExpression leftConst3 && leftConst3.Value < 0)
            {
                return new SubtractExpression(Right, new ConstantExpression(-leftConst3.Value)).Simplify();
            }
            if (Right is ConstantExpression rightConst3 && rightConst3.Value < 0)
            {
                return new SubtractExpression(Left, new ConstantExpression(-rightConst3.Value)).Simplify();
            }

            //Nested Adds: (x + 2) + 3 = x + 5 or 2 + (x + 3) = x + 5
            if (Left is AddExpression leftAdd && leftAdd.Right is ConstantExpression leftAddRightConst && Right is ConstantExpression rc)
            {
                return new AddExpression(leftAdd.Left, new ConstantExpression(leftAddRightConst.Value + rc.Value)).Simplify();
            }
            if (Right is AddExpression rightAdd && rightAdd.Left is ConstantExpression rightAddLeftConst && Left is ConstantExpression lc)
            {
                return new AddExpression(rightAdd.Right, new ConstantExpression(rightAddLeftConst.Value + lc.Value)).Simplify();
            }

            //Hierarchical Reorder: 2 + x^2 = x^2 + 2
            if (Left is ConstantExpression && !(Right is ConstantExpression))
            {
                return new AddExpression(Right, Left).Simplify();
            }

            //No further simplification possible
            return new AddExpression(Left, Right);
        }

        public override string Explain(string exp)
        {
            return $"{Left} + {Right}: The derivative of a sum is the sum of the derivatives, so the derivative of {Left} + {Right} is the derivative of {Left} plus the derivative of {Right}." + Environment.NewLine + Left.Explain(Left.ToString()) + Right.Explain(Right.ToString());
        }
    }
}
