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

    public override Expression Simplify()
    {
        // Step 1: Simplify numerator and denominator recursively
        var num = Numerator.Simplify();
        var den = Denominator.Simplify();

        // Step 2: Handle simple edge cases
        if (num is ConstantExpression nc && nc.Value == 0)
            return new ConstantExpression(0); // 0 / anything = 0

        if (den is ConstantExpression dc1 && dc1.Value == 1)
            return num; // divide by 1

        if (num.ToString() == den.ToString())
            return new ConstantExpression(1); // x/x = 1

        // Step 3: Flatten numerator and denominator and count powers
        var numDict = FlattenAndCountPowers(num);
        var denDict = FlattenAndCountPowers(den);

        // Step 4: Cancel common factors
        foreach (var key in denDict.Keys.ToList())
        {
            if (numDict.ContainsKey(key))
            {
                int cancel = Math.Min(numDict[key].count, denDict[key].count);
                numDict[key] = (numDict[key].expr, numDict[key].count - cancel);
                denDict[key] = (denDict[key].expr, denDict[key].count - cancel);
            }
        }

        // Step 5: Combine constants
        double numConst = 1, denConst = 1;
        foreach (var kvp in numDict.ToList())
        {
            if (kvp.Value.expr is ConstantExpression c)
            {
                numConst *= Math.Pow(c.Value, kvp.Value.count);
                numDict.Remove(kvp.Key);
            }
        }
        foreach (var kvp in denDict.ToList())
        {
            if (kvp.Value.expr is ConstantExpression c)
            {
                denConst *= Math.Pow(c.Value, kvp.Value.count);
                denDict.Remove(kvp.Key);
            }
        }

        numConst /= denConst;

        // Step 6: Rebuild numerator and denominator
        Expression newNum = BuildProductFromCounts(numDict, numConst);
        Expression newDen = BuildProductFromCounts(denDict, 1); // denominator constants already applied

        // Step 7: If denominator is 1, return numerator
        if (newDen is ConstantExpression dc && dc.Value == 1)
            return newNum;

        return new DivideExpression(newNum, newDen);
    }

    // --- Helpers inside DivideExpression ---

    // Flatten products and count powers
    private Dictionary<string, (Expression expr, int count)> FlattenAndCountPowers(Expression expr)
    {
        var dict = new Dictionary<string, (Expression, int)>();
        void helper(Expression e)
        {
            if (e is ProductExpression p)
            {
                helper(p.Left);
                helper(p.Right);
            }
            else if (e is PowerExpression pow)
            {
                string key = pow.Base.ToString();
                if (dict.ContainsKey(key)) dict[key] = (pow.Base, dict[key].Item2 + pow.Exponent);
                else dict[key] = (pow.Base, pow.Exponent);
            }
            else
            {
                string key = e.ToString();
                if (dict.ContainsKey(key)) dict[key] = (e, dict[key].Item2 + 1);
                else dict[key] = (e, 1);
            }
        }
        helper(expr);
        return dict;
    }

    // Rebuild a product from factor counts and constant
    private Expression BuildProductFromCounts(Dictionary<string, (Expression expr, int count)> dict, double constant)
    {
        Expression result = constant != 1 ? new ConstantExpression(constant) : null;

        foreach (var kvp in dict)
        {
            var e = kvp.Value.expr;
            int count = kvp.Value.count;
            if (count <= 0) continue;
            Expression factor = count == 1 ? e : new PowerExpression(e, count);
            if (result == null) result = factor;
            else result = new ProductExpression(result, factor);
        }

        return result ?? new ConstantExpression(1);
    }
}