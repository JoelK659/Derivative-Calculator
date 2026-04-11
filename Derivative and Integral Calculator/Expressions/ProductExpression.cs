using System;
using System.Collections.Generic;
using System.Text;

namespace Derivative_and_Integral_Calculator.Expressions
{
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
            // Product rule: f'g + fg'
            return new AddExpression(
                new ProductExpression(Left.Differentiate(), Right),
                new ProductExpression(Left, Right.Differentiate())
            );
        }

        public override string ToString()
        {
            //return $"{Left}{Right}";
            return $"({Left} * {Right})";
        }

        public override Expression Simplify()
        {
            Left = Left.Simplify();
            Right = Right.Simplify();

            //Zero Rule: 0 * x = 0
            if (Left is ConstantExpression constExpr && constExpr.Value == 0)
            {
                return new ConstantExpression(0);
            }
            if (Right is ConstantExpression constExpr2 && constExpr2.Value == 0)
            {
                return new ConstantExpression(0);
            }

            //One Rule: 1 * x = x
            if (Left is ConstantExpression constExpr3 && constExpr3.Value == 1)
            {
                return Right;
            }
            if (Right is ConstantExpression constExpr4 && constExpr4.Value == 1)
            {
                return Left;
            }

            //Constant Rule: 2 * 3 = 6
            if (Left is ConstantExpression constExpr5 && Right is ConstantExpression constExpr6)
            {
                return new ConstantExpression(constExpr5.Value * constExpr6.Value);
            }

            //Nested Product: (x * y) * z = x * (y * z)
            if (Left is ProductExpression leftProd && leftProd.Right is ConstantExpression leftProdRightConst && Right is ConstantExpression rightConst)
            {
                return new ProductExpression(leftProd.Left, new ConstantExpression(leftProdRightConst.Value * rightConst.Value)).Simplify();
            }

            if (Right is ProductExpression rightProd && rightProd.Left is ConstantExpression rightProdLeftConst && Left is ConstantExpression leftConst)
            {
                return new ProductExpression(new ConstantExpression(rightProdLeftConst.Value * leftConst.Value), rightProd.Right).Simplify();
            }


            return new ProductExpression(Left, Right);
        }

        public override string Explain(string exp)
        {
            return $"{Left} * {Right}: The derivative of a product is given by the product rule, which states that the derivative of {Left} * {Right} is the derivative of {Left} times {Right} plus {Left} times the derivative of {Right}." + Environment.NewLine + Left.Explain(exp) + Right.Explain(exp);
        }
    }
}
