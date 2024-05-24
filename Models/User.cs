
namespace API.Models;
public class User 
{
    public User(string name, string eMail)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        EMail = eMail ?? throw new ArgumentNullException(nameof(eMail));
    }
    public User() 
    {
        Name = "";
        EMail = "";
    }

    public int Id {get; set;}
    public string Name { get; set; }
    public string EMail{ get; set;}
    public string Link {get; set;}
    public int Count {get; set;}
}