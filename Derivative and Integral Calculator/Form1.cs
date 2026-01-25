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
    }
}
