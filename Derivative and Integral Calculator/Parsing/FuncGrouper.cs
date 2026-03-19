using System;
using System.Collections.Generic;
using System.Text;

namespace Derivative_and_Integral_Calculator.Parsing
{
    /// <summary>
    /// Provides functionality to parse a mathematical expression string into a sequence of function tokens, identifying
    /// numbers, variables, operators, and supported mathematical functions.
    /// </summary>
    /// <remarks>The FuncGrouper class is designed to facilitate the lexical analysis of mathematical
    /// expressions for further processing, such as evaluation or symbolic manipulation. It supports recognition of
    /// standard mathematical operators, parentheses, and common functions such as sine, cosine, tangent, logarithm, and
    /// constants like e and pi. The class removes whitespace from the input expression and throws an exception if an
    /// unrecognized character is encountered. Instances of FuncGrouper are not thread-safe.</remarks>
    class FuncGrouper
    {
        private string input;
        private int position; // Current position in the input string

        public FuncGrouper(string input)
        {
            this.input = input.Replace(" ", ""); // Remove whitespace
            position = 0;
        }

        // Groups characters into meaningful tokens
        public List<Function> GroupCharacters()
        {
            position = 0;
            var groups = new List<Function>();

            // Iterate through the input string
            while (position < input.Length)
            {
                char currentChar = input[position];

                // Determine the type of character and group accordingly

                //Numbers
                if (char.IsDigit(currentChar) || currentChar == '.')
                {
                    groups.Add(ReadNumber());
                    continue;
                }

                //Variables
                if (char.IsLetter(currentChar))
                {
                    // Identifiers: variables or functions
                    if (char.IsLetter(currentChar))
                    {
                        int start = position;
                        char prevchar = input[position];

                        while (position < input.Length && char.IsLetter(input[position]))
                        {
                            position++;
                            
                        }
                            

                        string ident = input.Substring(start, position - start);

                        // Check if the identifier is a recognized function or constant, otherwise treat it as a variable
                        switch (ident)
                        {
                            case "sin": groups.Add(new Function(FunctionType.Sin, ident)); break;
                            case "cos": groups.Add(new Function(FunctionType.Cos, ident)); break;
                            case "tan": groups.Add(new Function(FunctionType.Tan, ident)); break;
                            case "sec": groups.Add(new Function(FunctionType.Sec, ident)); break;
                            case "csc": groups.Add(new Function(FunctionType.Csc, ident)); break;
                            case "cot": groups.Add(new Function(FunctionType.Cot, ident)); break;
                            //case "ln": groups.Add(new Function(FunctionType.Ln, ident)); break;
                            //case "log": groups.Add(new Function(FunctionType.Log, ident)); break;
                            //case "e": groups.Add(new Function(FunctionType.e, ident)); break;
                            default:
                                groups.Add(new Function(FunctionType.Variable, ident));
                                break;
                        }

                        continue;
                    }

                }

                //Single Character Operators and Functions
                switch (currentChar)
                {
                    case '+':
                        groups.Add(new Function(FunctionType.Plus, currentChar.ToString()));
                        break;
                    case '-':
                        groups.Add(new Function(FunctionType.Minus, currentChar.ToString()));
                        break;
                    case '*':
                        groups.Add(new Function(FunctionType.Multiply, currentChar.ToString()));
                        break;
                    case '/':
                        groups.Add(new Function(FunctionType.Divide, currentChar.ToString()));
                        break;
                    case '^':
                        groups.Add(new Function(FunctionType.Power, currentChar.ToString()));
                        break;
                    case '√':
                        groups.Add(new Function(FunctionType.SquareRoot, currentChar.ToString()));
                        break;
                    case 'e':
                        groups.Add(new Function(FunctionType.e, currentChar.ToString()));
                        break;
                    case 'π':
                        groups.Add(new Function(FunctionType.pi, currentChar.ToString()));
                        break;
                    case '(':
                        groups.Add(new Function(FunctionType.LeftParenthesis, currentChar.ToString()));
                        break;
                    case ')':
                        groups.Add(new Function(FunctionType.RightParenthesis, currentChar.ToString()));
                        break;
                    default:
                        throw new Exception($"Unrecognized character: {currentChar}");
                }

                //Multi-Character Functions
                switch (currentChar )
                {
                    case 's':
                        if (position + 2 < input.Length)
                        {
                            string func = input.Substring(position, 3);
                            if (func == "sin")
                            {
                                groups.Add(new Function(FunctionType.Sin, func));
                                position += 3;
                                continue;
                            }
                            else if (func == "sec")
                            {
                                groups.Add(new Function(FunctionType.Sec, func));
                                position += 3;
                                continue;
                            }
                        }
                        break;
                    case 'c':
                        if (position + 2 < input.Length)
                        {
                            string func = input.Substring(position, 3);
                            if (func == "cos")
                            {
                                groups.Add(new Function(FunctionType.Cos, func));
                                position += 3;
                                continue;
                            }
                            else if (func == "csc")
                            {
                                groups.Add(new Function(FunctionType.Csc, func));
                                position += 3;
                                continue;
                            }
                            else if(func == "cot")
                            {
                                groups.Add(new Function(FunctionType.Cot, func));
                                position += 3;
                                continue;
                            } 
                        }
                        break;
                    case 't':
                        if (position + 2 < input.Length)
                        {
                            string func = input.Substring(position, 3);
                            if (func == "tan")
                            {
                                groups.Add(new Function(FunctionType.Tan, func));
                                position += 3;
                                continue;
                            }
                        }
                        break;
                    case 'l':
                        if (position + 1 < input.Length)
                        {
                            string func = input.Substring(position, 2);
                            if (func == "ln")
                            {
                                groups.Add(new Function(FunctionType.Ln, func));
                                position += 2;
                                continue;
                            }
                            else if (func == "log")
                            {
                                groups.Add(new Function(FunctionType.Log, func));
                                position += 3;
                                continue;
                            }
                        }
                        break;
                }

                position++;
            }
            groups.Add(new Function(FunctionType.End, "")); // End of input marker (Remove when implementing implicit multiplication)
            return groups;
        }

        // Reads a number (including decimals) from the input
        private Function ReadNumber()
        {
            int start = position;
            // Move position forward while characters are digits or decimal points
            while (position < input.Length && (char.IsDigit(input[position]) || input[position] == '.'))
            {
                position++;
            }
            // Extract the number substring
            string numberText = input.Substring(start, position - start);
            return new Function(FunctionType.Number, numberText);
        }
    }
}
