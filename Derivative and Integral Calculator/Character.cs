using System;
using System.Collections.Generic;
using System.Text;

namespace Derivative_and_Integral_Calculator
{
    class Character
    {
        public CharacterType Type;
        public string Text;

        public Character(CharacterType type, string text)
        {
            Type = type;
            Text = text;
        }
    }
}
