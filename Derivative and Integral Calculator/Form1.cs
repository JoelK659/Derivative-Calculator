using System.Security.Cryptography.X509Certificates;
using Derivative_and_Integral_Calculator.Expressions;
using Derivative_and_Integral_Calculator.Parsing;

namespace Derivative_and_Integral_Calculator
{
    public partial class Form1 : Form
    {
        FuncGrouper functionGrouper;
        List<Function> functions;
        Parser functionParser;
        Expression expression;
        Expression derivative;
        Expression simplified;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ExplanationButton.Visible = false;

            //foreach (var f in functions)
            //{
            //    Console.Text += f.Type + " ";
            //}
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
            Console.Text += "√(";
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
            ExplanationBox.Text += Globals.explanationText + $"Final Product: {simplified.ToString()}";
            ExplanationButton.Enabled = false;
        }

        private void EnterButton_Click(object sender, EventArgs e)
        {
            Globals.explanationText = "";
            ExplanationBox.Text = "";
            functionGrouper = new FuncGrouper(Console.Text);

            functions = functionGrouper.GroupCharacters();

            functionParser = new Parser(functions);

            expression = functionParser.Parse();

            Globals.explanationText = expression.Explain(Console.Text);

            derivative = expression.Differentiate();

            simplified = derivative.Simplify();

            Console.Text = simplified.ToString();

            ExplanationButton.Visible = true;
            ExplanationButton.Enabled = true;
        }

        private void Clear_Button_Click(object sender, EventArgs e)
        {
            Console.Text = "";
        }

        private void Console_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Suppress the default function of the Enter key (e.g., adding a new line)
                EnterButton_Click(sender, e);
                e.Handled = true;

            }
        }
    }


}
