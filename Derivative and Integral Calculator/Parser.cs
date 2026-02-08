using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Derivative_and_Integral_Calculator
{
    class Parser
    {
        private List<Function> Functions;
        private int position;
        
        public Parser(List<Function> functions)
        {
            Functions = functions;
            position = 0;
        }


        // Entry point for parsing the expression
        public Expression Parse()
        {
            Expression expression = ParseExpression();
            Consume(FunctionType.End);
            return expression;
        }

        // Recursive descent parsing methods

        // Parses addition and subtraction
        private Expression ParseExpression()
        {
            Expression expression = ParseTerm();

            while (Match(FunctionType.Plus, FunctionType.Minus))
            {
                Function op = Previous();
                Expression right = ParseTerm();

                if(op.Type == FunctionType.Plus)
                {
                    expression = new AddExpression(expression, right);
                }
                else
                {
                    expression = new AddExpression(expression, new ProductExpression(new ConstantExpression(-1), right));
                }
            }
            return expression;
        }

        // Parses multiplication
        private Expression ParseTerm()
        {
            Expression expression = ParseFactor();

            while (Match(FunctionType.Multiply))
            {
                Expression right = ParseFactor();
                expression = new ProductExpression(expression, right);
            }
            return expression;
        }

        // Parses exponentiation
        private Expression ParseFactor()
        {
            Expression expression = ParseBase();

            if (Match(FunctionType.Power))
            {
                Expression exponent = ParseFactor();

                if (exponent is ConstantExpression c)
                {
                    return new PowerExpression(expression, (int)c.Value);
                }
                else
                {
                    throw new Exception("Exponent must be a constant.");
                }
            }
            return expression;
        }

        // Parses numbers, variables, and parenthesized expressions
        private Expression ParseBase()
        {
            if (Match(FunctionType.Minus))
            {
                return new ProductExpression(
                    new ConstantExpression(-1),
                    ParseBase()
                );
            }

            if (Match(FunctionType.Number))
            {
                return new ConstantExpression(double.Parse(Previous().Text));
            }

            if (Match(FunctionType.Variable))
            {
                return new VariableExpression();
            }

            if(Match(FunctionType.LeftParenthesis))
            {
                Expression expression = ParseExpression();
                Consume(FunctionType.RightParenthesis);
                return expression;
            }

            throw new Exception("Unexpected function: ");
        }

        // Helper methods for parsing

        // Checks if the current function matches any of the given types and advances the position if it does
        private bool Match(params FunctionType[] types)
        {
            foreach (var type in types)
            {
                if (Check(type))
                {
                    position++;
                    return true;
                }
            }
            return false;
        }

        // Checks if the current function matches the given type
        private bool Check(FunctionType type)
        {
            if (position >= Functions.Count)
                return false;

            return Functions[position].Type == type;
        }

        // Returns the previous function (the one that was just consumed)
        private Function Previous()
        {
            return Functions[position - 1];
        }

        // Consumes the current function if it matches the given type, otherwise throws an exception
        private void Consume(FunctionType type)
        {
            if (Check(type))
            {
                position++;
                return;
            }
            else
            {
                throw new Exception($"Expected function of type {type} but found {Functions[position].Type}");
            }
        }


    }
}
