namespace WebApplication2.Models;

public class User
{
    public required Guid id { get; set; }
    public string name { get; set; }
    public required string email { get; set; }
}