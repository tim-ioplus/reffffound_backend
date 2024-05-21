
namespace API.Models;
public class User 
{
    public User(string name, string eMail, string password, bool active = false)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        EMail = eMail ?? throw new ArgumentNullException(nameof(eMail));
        Password = password ?? throw new ArgumentNullException(nameof(password));
        Active = active;
    }
    public User() 
    {
        Name = "";
        EMail = "";
        Password = "";
    }

    public int Id {get; set;}
    public string Name { get; set; }
    public string EMail{ get; set;}
    public string Password {get; set; }
    public bool Active{ get; set;}
}