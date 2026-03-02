using System;
using System.Collections.Generic;
using System.Text;

namespace Derivative_and_Integral_Calculator.Parsing
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

        // Main method to group characters and handle implicit multiplication
        /*
        public List<Function> FinalGroup()
        {
            List<Function> functions = GroupCharacters();
            functions = InsertImplicitMultiplication(functions);
            functions.Add(new Function(FunctionType.End, "")); // End of input marker
            return functions;
        }
        */

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

                        while (position < input.Length && char.IsLetter(input[position]))
                            position++;

                        string ident = input.Substring(start, position - start);

                        switch (ident)
                        {
                            case "sin": groups.Add(new Function(FunctionType.Sin, ident)); break;
                            case "cos": groups.Add(new Function(FunctionType.Cos, ident)); break;
                            case "tan": groups.Add(new Function(FunctionType.Tan, ident)); break;
                            case "sec": groups.Add(new Function(FunctionType.Sec, ident)); break;
                            case "csc": groups.Add(new Function(FunctionType.Csc, ident)); break;
                            case "cot": groups.Add(new Function(FunctionType.Cot, ident)); break;
                            case "ln": groups.Add(new Function(FunctionType.Ln, ident)); break;
                            case "log": groups.Add(new Function(FunctionType.Log, ident)); break;
                            case "e": groups.Add(new Function(FunctionType.e, ident)); break;
                            case "pi": groups.Add(new Function(FunctionType.pi, ident)); break;
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

        // Inserts implicit multiplication operators where needed (e.g., between a number and a variable, or between a variable and a function)
        /*
        private List<Function> InsertImplicitMultiplication(List<Function> functions)
        {
            var result = new List<Function>();

            for(int i = 0; i < functions.Count - 1; i++)
            {
                Function current = functions[i];
                Function next = functions[i + 1];

                result.Add(current);

                bool needsMultiplication =
                    (current.Type == FunctionType.Number ||
                    current.Type == FunctionType.Variable ||
                    current.Type == FunctionType.RightParenthesis) 
                    &&
                    (next.Type == FunctionType.Number ||
                    next.Type == FunctionType.Variable ||
                    next.Type == FunctionType.LeftParenthesis);

                if(needsMultiplication)
                {
                    result.Add(new Function(FunctionType.Multiply, "*"));
                }
            }


        }
        */

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
