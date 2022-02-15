namespace CleanArchitecture.Domain.ValueObjects;

public class Balance : ValueObject
{

    public decimal Value { get; private set; }
    public Balance() { }

    public void ReduceWalletBalance(Amount amount)
    {
        if (Value < amount.Value)
            throw new NullReferenceException();

        Value -= amount.Value;
    }

    public void IncreaseWalletBalance(Amount amount)
    {
        if (amount.Value <= 0)
            throw new NullReferenceException();

        Value += amount.Value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }


}
