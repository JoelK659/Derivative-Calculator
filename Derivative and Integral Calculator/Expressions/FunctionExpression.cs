using System;
using System.Collections.Generic;
using System.Text;
using Derivative_and_Integral_Calculator.Parsing;

namespace Derivative_and_Integral_Calculator.Expressions
{
    class FunctionExpression : Expression
    {
        public Function func;
        public Expression innerExp;

        public FunctionExpression(FunctionType funcType, Expression exp)
        {
            func = new Function(funcType, funcType.ToString());
            innerExp = exp;
        }

        public override Expression Differentiate()
        {
            switch(func.Type)
            {
                case FunctionType.Sin:
                    return new ProductExpression(new FunctionExpression(FunctionType.Cos, innerExp), innerExp.Differentiate());
                case FunctionType.Cos:
                    return new ProductExpression(new ProductExpression(new ConstantExpression(-1), new FunctionExpression(FunctionType.Sin, innerExp)), innerExp.Differentiate());
                case FunctionType.Tan:
                    return new ProductExpression(new PowerExpression(new FunctionExpression(FunctionType.Sec, innerExp), 2), innerExp.Differentiate());
                case FunctionType.Csc:
                    return new ProductExpression(new ProductExpression(new ProductExpression(new ConstantExpression(-1), new FunctionExpression(FunctionType.Csc, innerExp)), new FunctionExpression(FunctionType.Cot, innerExp)), innerExp.Differentiate());
                case FunctionType.Sec:
                    return new ProductExpression(new ProductExpression(new FunctionExpression(FunctionType.Sec, innerExp), new FunctionExpression(FunctionType.Tan, innerExp)), innerExp.Differentiate());
                case FunctionType.Cot:
                    return new ProductExpression(new ProductExpression(new ConstantExpression(-1), new PowerExpression(new FunctionExpression(FunctionType.Csc, innerExp), 2)), innerExp.Differentiate());
                case FunctionType.Ln:
                    return new ProductExpression(new PowerExpression(innerExp, -1), innerExp.Differentiate());
                case FunctionType.SquareRoot:
                    return new ProductExpression(new PowerExpression(innerExp, -0.5), innerExp.Differentiate());
                default:
                    throw new NotImplementedException();
            }
        }

        public override string ToString()
        {
            return $"{func.Text}({innerExp.ToString()})";
        }

        public override Expression Simplify()
        {
            innerExp = innerExp.Simplify();
            return this;
        }

        public override string Explain(string exp)
        {
            return $"{func.Text}{innerExp.ToString()}: The derivative of {func.Text} is given by the chain rule, which states that the derivative of {func.Text}{innerExp.ToString()} is the derivative of the outer function evaluated at the inner function, multiplied by the derivative of the inner function." + Environment.NewLine + innerExp.Explain(exp);
        }
    }
}
