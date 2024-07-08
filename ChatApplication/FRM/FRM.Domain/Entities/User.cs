namespace FRM.Domain.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!; 
    public virtual List<Message> Messages { get; set; } = new();
}
