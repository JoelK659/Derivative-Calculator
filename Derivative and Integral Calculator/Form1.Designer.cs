namespace Derivative_and_Integral_Calculator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SineButton = new Button();
            CosineButton = new Button();
            TangentButton = new Button();
            CosecantButton = new Button();
            SecantButton = new Button();
            CotangentButton = new Button();
            NaturalLogButton = new Button();
            LogButton = new Button();
            EButton = new Button();
            SqrtButton = new Button();
            ExpButton = new Button();
            PiButton = new Button();
            Console = new TextBox();
            ExplanationButton = new Button();
            ExplanationBox = new TextBox();
            SuspendLayout();
            // 
            // SineButton
            // 
            SineButton.Location = new Point(18, 287);
            SineButton.Name = "SineButton";
            SineButton.Size = new Size(75, 23);
            SineButton.TabIndex = 0;
            SineButton.Text = "sin";
            SineButton.UseVisualStyleBackColor = true;
            SineButton.Click += SineButton_Click;
            // 
            // CosineButton
            // 
            CosineButton.Location = new Point(113, 287);
            CosineButton.Name = "CosineButton";
            CosineButton.Size = new Size(75, 23);
            CosineButton.TabIndex = 1;
            CosineButton.Text = "cos";
            CosineButton.UseVisualStyleBackColor = true;
            CosineButton.Click += CosineButton_Click;
            // 
            // TangentButton
            // 
            TangentButton.Location = new Point(207, 287);
            TangentButton.Name = "TangentButton";
            TangentButton.Size = new Size(75, 23);
            TangentButton.TabIndex = 2;
            TangentButton.Text = "tan";
            TangentButton.UseVisualStyleBackColor = true;
            TangentButton.Click += TangentButton_Click;
            // 
            // CosecantButton
            // 
            CosecantButton.Location = new Point(18, 330);
            CosecantButton.Name = "CosecantButton";
            CosecantButton.Size = new Size(75, 23);
            CosecantButton.TabIndex = 3;
            CosecantButton.Text = "csc";
            CosecantButton.UseVisualStyleBackColor = true;
            CosecantButton.Click += CosecantButton_Click;
            // 
            // SecantButton
            // 
            SecantButton.Location = new Point(113, 330);
            SecantButton.Name = "SecantButton";
            SecantButton.Size = new Size(75, 23);
            SecantButton.TabIndex = 4;
            SecantButton.Text = "sec";
            SecantButton.UseVisualStyleBackColor = true;
            SecantButton.Click += SecantButton_Click;
            // 
            // CotangentButton
            // 
            CotangentButton.Location = new Point(207, 330);
            CotangentButton.Name = "CotangentButton";
            CotangentButton.Size = new Size(75, 23);
            CotangentButton.TabIndex = 5;
            CotangentButton.Text = "cot";
            CotangentButton.UseVisualStyleBackColor = true;
            CotangentButton.Click += CotangentButton_Click;
            // 
            // NaturalLogButton
            // 
            NaturalLogButton.Location = new Point(18, 370);
            NaturalLogButton.Name = "NaturalLogButton";
            NaturalLogButton.Size = new Size(75, 23);
            NaturalLogButton.TabIndex = 6;
            NaturalLogButton.Text = "ln";
            NaturalLogButton.UseVisualStyleBackColor = true;
            NaturalLogButton.Click += NaturalLogButton_Click;
            // 
            // LogButton
            // 
            LogButton.Location = new Point(113, 370);
            LogButton.Name = "LogButton";
            LogButton.Size = new Size(75, 23);
            LogButton.TabIndex = 7;
            LogButton.Text = "log";
            LogButton.UseVisualStyleBackColor = true;
            LogButton.Click += LogButton_Click;
            // 
            // EButton
            // 
            EButton.Location = new Point(207, 370);
            EButton.Name = "EButton";
            EButton.Size = new Size(75, 23);
            EButton.TabIndex = 8;
            EButton.Text = "e";
            EButton.UseVisualStyleBackColor = true;
            EButton.Click += EButton_Click;
            // 
            // SqrtButton
            // 
            SqrtButton.Location = new Point(18, 408);
            SqrtButton.Name = "SqrtButton";
            SqrtButton.Size = new Size(75, 23);
            SqrtButton.TabIndex = 9;
            SqrtButton.Text = "√";
            SqrtButton.UseVisualStyleBackColor = true;
            SqrtButton.Click += SqrtButton_Click;
            // 
            // ExpButton
            // 
            ExpButton.Location = new Point(113, 408);
            ExpButton.Name = "ExpButton";
            ExpButton.Size = new Size(75, 23);
            ExpButton.TabIndex = 10;
            ExpButton.Text = "xʸ";
            ExpButton.UseVisualStyleBackColor = true;
            ExpButton.Click += ExpButton_Click;
            // 
            // PiButton
            // 
            PiButton.Location = new Point(207, 408);
            PiButton.Name = "PiButton";
            PiButton.Size = new Size(75, 23);
            PiButton.TabIndex = 11;
            PiButton.Text = "π";
            PiButton.UseVisualStyleBackColor = true;
            PiButton.Click += PiButton_Click;
            // 
            // Console
            // 
            Console.Font = new Font("Verdana", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Console.Location = new Point(45, 67);
            Console.Multiline = true;
            Console.Name = "Console";
            Console.Size = new Size(713, 123);
            Console.TabIndex = 12;
            // 
            // ExplanationButton
            // 
            ExplanationButton.BackColor = Color.LightSkyBlue;
            ExplanationButton.FlatStyle = FlatStyle.Popup;
            ExplanationButton.Location = new Point(590, 211);
            ExplanationButton.Name = "ExplanationButton";
            ExplanationButton.Size = new Size(168, 66);
            ExplanationButton.TabIndex = 13;
            ExplanationButton.Text = "Explain!";
            ExplanationButton.UseVisualStyleBackColor = false;
            ExplanationButton.Click += ExplanationButton_Click;
            // 
            // ExplanationBox
            // 
            ExplanationBox.Location = new Point(393, 301);
            ExplanationBox.Multiline = true;
            ExplanationBox.Name = "ExplanationBox";
            ExplanationBox.ReadOnly = true;
            ExplanationBox.Size = new Size(395, 137);
            ExplanationBox.TabIndex = 14;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ExplanationBox);
            Controls.Add(ExplanationButton);
            Controls.Add(Console);
            Controls.Add(PiButton);
            Controls.Add(ExpButton);
            Controls.Add(SqrtButton);
            Controls.Add(EButton);
            Controls.Add(LogButton);
            Controls.Add(NaturalLogButton);
            Controls.Add(CotangentButton);
            Controls.Add(SecantButton);
            Controls.Add(CosecantButton);
            Controls.Add(TangentButton);
            Controls.Add(CosineButton);
            Controls.Add(SineButton);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button SineButton;
        private Button CosineButton;
        private Button TangentButton;
        private Button CosecantButton;
        private Button SecantButton;
        private Button CotangentButton;
        private Button NaturalLogButton;
        private Button LogButton;
        private Button EButton;
        private Button SqrtButton;
        private Button ExpButton;
        private Button PiButton;
        private TextBox Console;
        private Button ExplanationButton;
        private TextBox ExplanationBox;
    }
}
