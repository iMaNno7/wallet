namespace CleanArchitecture.Domain.Entities;

public class Category : AuditableEntity
{
    public string? Title { get; set; }

    public Colour Colour { get; set; } = Colour.White;


    public IList<Transaction> Items { get; private set; } = new List<Transaction>();
}
