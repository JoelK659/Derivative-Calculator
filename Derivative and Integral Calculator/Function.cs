using System;
using System.Collections.Generic;
using System.Text;

namespace Derivative_and_Integral_Calculator
{
    class Function
    {
        public FunctionType Type;
        public string Text;

        public Function(FunctionType type, string text)
        {
            Type = type;
            Text = text;
        }
    }
}
