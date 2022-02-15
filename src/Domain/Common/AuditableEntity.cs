namespace CleanArchitecture.Domain.Common;

public abstract class BaseEntity
{
    public DateTime Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
}
public class AuditableEntity<T> : BaseEntity
{
    public T Id { get; set; }
}
public class AuditableEntity : AuditableEntity<int>
{
}
