using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Derivative_and_Integral_Calculator.Expressions;

namespace Derivative_and_Integral_Calculator.Parsing
{

    // The Parser class takes a list of functions (tokens) and constructs an expression tree that represents the mathematical expression. It uses recursive descent parsing to handle operator precedence and associativity.
    //Ex. for the expression "2x + 3", the FuncGrouper would produce a list of functions: [Number(2), Variable(x), Plus, Number(3)].
    //The Parser would then take this list and construct an expression tree that represents the addition of the product of 2 and x with 3.
    class Parser
    {
        private List<Function> Functions;
        private int position;
        private FunctionType? previousFunction = null; 
        
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
            // Start by parsing the first term (which could be a product or a factor)
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
                    expression = new SubtractExpression(expression, right);
                }
            }
            return expression;
        }

        // Parses multiplication and division
        private Expression ParseTerm()
        {
            
            Expression expression = ParseFactor();

            while (true)
            {
                // Explicit multiplication
                if (Match(FunctionType.Multiply))
                {
                    Expression right = ParseFactor();
                    expression = new ProductExpression(expression, right);
                }

                // Division
                else if (Match(FunctionType.Divide))
                {
                    Expression right = ParseFactor(); // Parse denominator as a single factor
                    expression = new DivideExpression(expression, right);
                }

                // Implicit multiplication (skip if previous token was divide)
                else if (NextStartsFactor())
                {
                    Expression right = ParseFactor();
                    expression = new ProductExpression(expression, right);
                }

                else
                {
                    break;
                }
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
                return new VariableExpression(Previous().Text);
            }
            
            switch (Functions[position].Type)
            {
                case FunctionType.Sin:
                case FunctionType.Cos:
                case FunctionType.Tan:
                case FunctionType.Csc:
                case FunctionType.Sec:
                case FunctionType.Cot:
                //case FunctionType.Ln:
                //case FunctionType.Log:
                //case FunctionType.SquareRoot:
            }

            if (Match(FunctionType.LeftParenthesis))
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
                    previousFunction = Functions[position].Type;
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
                previousFunction = Functions[position].Type;
                position++;
                return;
            }
            else
            {
                throw new Exception($"Expected function of type {type} but found {Functions[position].Type}");
            }
        }
        // Checks if the next function starts a factor (number, variable, left parenthesis, or minus) Ex: 2x, (x + 1), -x
        private bool NextStartsFactor()
        {
            if (position >= Functions.Count)
            {
                return false;
            }

            var nextType = Functions[position].Type;

            // Only allow implicit multiplication for number, variable, left parenthesis, or functions
            // Do NOT allow it immediately after a division operator
            if (nextType == FunctionType.Number ||
                nextType == FunctionType.Variable ||
                nextType == FunctionType.LeftParenthesis ||
                IsFunctionType(nextType))
            {
                // Check that previous token was NOT a divide
                if (position > 0 && Functions[position - 1].Type == FunctionType.Divide)
                {
                    return false;
                }
                    

                return true;
            }

            return false;
        }

        // Helper to detect multi-character functions
        private bool IsFunctionType(FunctionType type)
        {
            switch (type)
            {
                case FunctionType.Sin:
                case FunctionType.Cos:
                case FunctionType.Tan:
                case FunctionType.Csc:
                case FunctionType.Sec:
                case FunctionType.Cot:
                case FunctionType.Ln:
                case FunctionType.Log:
                case FunctionType.SquareRoot:
                    return true;
                default:
                    return false;
            }
        }


    }
}
