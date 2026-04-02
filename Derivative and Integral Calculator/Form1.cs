using System.Security.Cryptography.X509Certificates;
using Derivative_and_Integral_Calculator.Expressions;
using Derivative_and_Integral_Calculator.Parsing;

namespace Derivative_and_Integral_Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ExplanationButton.Visible = false;
            
            string input = "sin(3x^2)";


            var functionGrouper = new FuncGrouper(input);
            List<Function> functions = functionGrouper.GroupCharacters();


            //foreach (var f in functions)
            //{
            //    Console.Text += f.Type + " ";
            //}

            var functionParser = new Parser(functions);

            Expression expression = functionParser.Parse();

            Expression derivative = expression.Differentiate();

            Expression simplified = derivative.Simplify();

            Console.Text = simplified.ToString();

            ExplanationButton.Visible = true;
        }

        internal static string Explanation(Expression expr, string exp)
        {
            switch (expr)
            {
                case ConstantExpression c:
                    return exp + $"The derivative of a constant is always 0, so the derivative of {c.Value} is 0.\n";
                case PowerExpression power:
                    return exp + $"The derivative of a power is given by the power rule, which states that the derivative of {exp} is {power.Exponent} times {power.Base} raised to the power of {power.Exponent - 1}, multiplied by the derivative of {power.Base}.\n";
                case VariableExpression v:
                    return exp + $"The derivative of a variable with respect to itself is always 1, so the derivative of {v.Name} is 1.\n";
                case AddExpression add:
                    return exp + $"The derivative of a sum is the sum of the derivatives, so the derivative of {exp} is the derivative of {add.Left} plus the derivative of {add.Right}.\n";
                case SubtractExpression sub:
                    return exp + $"The derivative of a difference is the difference of the derivatives, so the derivative of {exp} is the derivative of {sub.Left} minus the derivative of {sub.Right}.\n";
                case ProductExpression product:
                    return exp + $"The derivative of a product is given by the product rule, which states that the derivative of {exp} is the derivative of {product.Left} times {product.Right} plus {product.Left} times the derivative of {product.Right}.\n";
                case DivideExpression divide:
                    return exp + $"The derivative of a quotient is given by the quotient rule, which states that the derivative of {exp} is the derivative of {divide.Numerator} times {divide.Denominator} minus {divide.Numerator} times the derivative of {divide.Denominator}, all divided by {divide.Denominator} squared.\n";
                case FunctionExpression func:
                    return exp + $"The derivative of a function is given by the chain rule, which states that the derivative of {exp} is the derivative of the outer function evaluated at the inner function, multiplied by the derivative of the inner function.\n";
                default:
                    return exp + "\n";
            }
        }

        private void SineButton_Click(object sender, EventArgs e)
        {
            Console.Text += "sin(";
        }

        private void CosineButton_Click(object sender, EventArgs e)
        {
            Console.Text += "cos(";
        }

        private void TangentButton_Click(object sender, EventArgs e)
        {
            Console.Text += "tan(";
        }

        private void CosecantButton_Click(object sender, EventArgs e)
        {
            Console.Text += "csc(";
        }

        private void SecantButton_Click(object sender, EventArgs e)
        {
            Console.Text += "sec(";
        }

        private void CotangentButton_Click(object sender, EventArgs e)
        {
            Console.Text += "cot(";
        }

        private void NaturalLogButton_Click(object sender, EventArgs e)
        {
            Console.Text += "ln(";
        }

        private void LogButton_Click(object sender, EventArgs e)
        {
            Console.Text += "log(";
        }

        private void EButton_Click(object sender, EventArgs e)
        {
            Console.Text += "e";
        }

        private void SqrtButton_Click(object sender, EventArgs e)
        {
            Console.Text += "sqrt(";
        }

        private void ExpButton_Click(object sender, EventArgs e)
        {
            Console.Text += "^";
        }

        private void PiButton_Click(object sender, EventArgs e)
        {
            Console.Text += "π";
        }

        private void ExplanationButton_Click(object sender, EventArgs e)
        {
            
        }
    }


}
