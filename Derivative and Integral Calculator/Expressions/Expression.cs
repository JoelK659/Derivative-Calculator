using System;
using System.Collections.Generic;
using System.Text;

namespace Derivative_and_Integral_Calculator.Expressions
{
    abstract class Expression
    {
        //Each expression should know how to differentiate itself
        public abstract Expression Differentiate();
        //Each expression should know how to represent itself as a string
        public abstract override string ToString();
        //Each expression should know how to simplify itself
        public abstract Expression Simplify();

        public abstract string Explain(string exp);
    }
}
