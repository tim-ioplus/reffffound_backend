
using System.Net.Sockets;

namespace API.Models;
public class User 
{
    public User( string eMail, string name="", string link="") 
    {
        EMail = eMail ?? throw new ArgumentNullException(nameof(eMail));
        Name = name;
        Link = link;
    }
    public User() 
    {
        Name = "";
        EMail = "";
        Link = "";
    }

    public int Id {get; set;}
    public string Name { get; set; }
    public string EMail{ get; set;}
    public string Link {get; set;}
    public int Count {get; set;}
}