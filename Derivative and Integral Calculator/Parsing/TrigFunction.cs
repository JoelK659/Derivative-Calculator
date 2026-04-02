using System;
using System.Collections.Generic;
using System.Text;

namespace Derivative_and_Integral_Calculator.Parsing
{
    class TrigFunction : Function
    {
        public FunctionType innerExp;
        public string innerText;

        public TrigFunction(FunctionType type, string text, FunctionType innerExp, string innerText) : base(type, text)
        {
            this.innerExp = innerExp;
            this.innerText = innerText;
        }

    }
}
