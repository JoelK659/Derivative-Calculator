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
        return $"({Numerator} / {Denominator})";
    }

    //public override Expression Simplify()
    //{
        
    //}
}