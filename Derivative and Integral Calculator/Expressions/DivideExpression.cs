using Derivative_and_Integral_Calculator.Expressions;

class DivideExpression : Expression
{
    public Expression Numerator;
    public Expression Denominator;

    public DivideExpression(Expression numerator, Expression denominator)
    {
        Numerator = numerator;
        Denominator = denominator;
    }

    public override Expression Differentiate()
    {
        // Quotient Rule: (f/g)' = (f' * g - f * g') / (g^2)
        return new DivideExpression(
            new AddExpression(
                new ProductExpression(Numerator.Differentiate(), Denominator),
                new ProductExpression(new ConstantExpression(-1), new ProductExpression(Numerator, Denominator.Differentiate()))
            ),
            new PowerExpression(Denominator, 2)
        );
    }

    public override string ToString()
    {
       // return $"{Numerator} / {Denominator}";
        return $"({Numerator} / {Denominator})";

    }

    public override Expression Simplify()
    {
        Numerator = Numerator.Simplify();
        Denominator = Denominator.Simplify();

        // Zero rule: 0/ x = 0
        if (Numerator is ConstantExpression numConst && numConst.Value == 0)
        {
            return new ConstantExpression(0);
        }

        // One rule: x / 1 = x
        if (Denominator is ConstantExpression denomConst && denomConst.Value == 1)
        {
            return Numerator;
        }

        //Constant rule: 6 / 3 = 2
        if (Numerator is ConstantExpression numConst2 && Denominator is ConstantExpression denomConst2)
        {
            return new ConstantExpression(Math.Round((numConst2.Value / denomConst2.Value), 2));
        }

        return new DivideExpression(Numerator, Denominator);
    }

    public override string Explain(string exp)
    {
        return $"{Numerator} / {Denominator}: The derivative of a quotient is given by the quotient rule, which states that the derivative of {Numerator} / {Denominator} is the derivative of {Numerator} times {Denominator} minus {Numerator} times the derivative of {Denominator}, all divided by {Denominator} squared." + Environment.NewLine + Numerator.Explain(exp) + Denominator.Explain(exp);
    }

}