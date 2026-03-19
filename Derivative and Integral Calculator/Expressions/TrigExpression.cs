using System;
using System.Collections.Generic;
using System.Text;
using Derivative_and_Integral_Calculator.Parsing;

namespace Derivative_and_Integral_Calculator.Expressions
{
    class TrigExpression : Expression
    {
        public Function trigFunc;
        public Expression innerExp;

        public TrigExpression(FunctionType funcType, Expression exp)
        {
            trigFunc = new Function(funcType, funcType.ToString());
            innerExp = exp;
        }

        public override Expression Differentiate()
        {
            switch(trigFunc.Type)
            {
                case FunctionType.Sin:
                    return new ProductExpression(new TrigExpression(FunctionType.Cos, innerExp), innerExp.Differentiate());
                case FunctionType.Cos:
                    return new ProductExpression(new ProductExpression(new ConstantExpression(-1), new TrigExpression(FunctionType.Sin, innerExp)), innerExp.Differentiate());
                case FunctionType.Tan:
                    return new ProductExpression(new PowerExpression(new TrigExpression(FunctionType.Sec, innerExp), 2), innerExp.Differentiate());
                case FunctionType.Csc:
                    return new ProductExpression(new ProductExpression(new ProductExpression(new ConstantExpression(-1), new TrigExpression(FunctionType.Csc, innerExp)), new TrigExpression(FunctionType.Cot, innerExp)), innerExp.Differentiate());
                case FunctionType.Sec:
                    return new ProductExpression(new ProductExpression(new TrigExpression(FunctionType.Sec, innerExp), new TrigExpression(FunctionType.Tan, innerExp)), innerExp.Differentiate());
                case FunctionType.Cot:
                    return new ProductExpression(new ProductExpression(new ConstantExpression(-1), new PowerExpression(new TrigExpression(FunctionType.Csc, innerExp), 2)), innerExp.Differentiate());
                default:
                    throw new NotImplementedException();
            }
        }

        public override string ToString()
        {
            return $"{trigFunc.Text}({innerExp.ToString()})";
        }

        public override Expression Simplify()
        {
            innerExp = innerExp.Simplify();
            return this;
        }

        public override string Explanation()
        {
            switch (trigFunc.Type)
            {
                case FunctionType.Sin:
                    return $"The derivative of sin(u) is cos(u) * u'. So, we switch sin({innerExp}) to cos({innerExp}) and then multiply that by the derivative of {innerExp}";
                case FunctionType.Cos:
                    return $"The derivative of cos(u) is -sin(u) * u'. So, we switch cos({innerExp}) to -sin({innerExp}) and then multiply that by the derivative of {innerExp}";
                case FunctionType.Tan:
                    return $"The derivative of tan(u) is sec^2(u) * u'. So, we switch tan({innerExp}) to sec^2({innerExp}) and then multiply that by the derivative of {innerExp}";
                case FunctionType.Csc:
                    return $"The derivative of csc(u) is -csc(u)cot(u) * u'. So, we switch csc({innerExp}) to -csc({innerExp})cot({innerExp}) and then multiply that by the derivative of {innerExp}";
                case FunctionType.Sec:
                    return $"The derivative of sec(u) is sec(u)tan(u) * u'. So, we switch sec({innerExp}) to sec({innerExp})tan({innerExp}) and then multiply that by the derivative of {innerExp}";
                case FunctionType.Cot:
                    return $"The derivative of cot(u) is -csc^2(u) * u'. So, we switch cot({innerExp}) to -csc^2({innerExp}) and then multiply that by the derivative of {innerExp}";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
