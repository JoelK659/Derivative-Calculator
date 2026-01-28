using System;
using System.Collections.Generic;
using System.Text;

namespace Derivative_and_Integral_Calculator
{
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
                    groups.Add(new Function(FunctionType.Variable, currentChar.ToString()));
                    position++;
                    continue;
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
            groups.Add(new Function(FunctionType.End, "")); // End of input marker
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
