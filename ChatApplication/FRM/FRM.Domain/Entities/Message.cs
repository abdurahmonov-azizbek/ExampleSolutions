namespace FRM.Domain.Entities;

public class Message : BaseEntity
{
    public string Content { get; set; } = default!;
    public Guid UserId { get; set; }
    public virtual User? User { get; set; }
}
