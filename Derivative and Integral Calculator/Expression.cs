using System;
using System.Collections.Generic;
using System.Text;

namespace Derivative_and_Integral_Calculator
{
    abstract class Expression
    {
        public abstract Expression Differentiate();
        public abstract override string ToString();

    }
}
