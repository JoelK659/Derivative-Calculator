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
            //return $"{Left} - {Right}";
            return $"({Left} - {Right})";
        }

        public override Expression Simplify()
        {
            Left = Left.Simplify();
            Right = Right.Simplify();

            //Zero Rule ex : 0 + x = x
            if (Left is ConstantExpression leftConst && leftConst.Value == 0)
            {
                return new ProductExpression(new ConstantExpression(-1), Right);
            }
            if (Right is ConstantExpression rightConst && rightConst.Value == 0)
            {
                return Left;
            }

            //Constant Rule ex : 3 - 2 = 1
            if (Left is ConstantExpression leftConst2 && Right is ConstantExpression rightConst2)
            {
                return new ConstantExpression(leftConst2.Value - rightConst2.Value);
            }

            //Negative Rule ex : x - (-y) = x + y
            if (Right is ProductExpression rightProduct && rightProduct.Left is ConstantExpression rightProductConst && rightProductConst.Value == -1)
            {
                return new AddExpression(Left, rightProduct.Right);
            }

            //No further simplification possible
            return new SubtractExpression(Left, Right);

        }
    }
}
