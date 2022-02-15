namespace CleanArchitecture.Domain.ValueObjects;

public class Amount : ValueObject
{

    public decimal Value { get; private set; }
    public Amount() { }
    public Amount(decimal value)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException();

        Value = value;
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }


}
