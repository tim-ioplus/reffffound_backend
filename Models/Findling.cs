namespace API.Models
{
    public class Findling
{
    public int Id {get; set;}
    public string? Guid {get; set;}
    public int UserId {get; set;}
    public string? UserName{ get; set;}
    public string? Url {get; set;}
    public string? Title {get; set;}
    // public Bitmap? Image {get; set;}
    public string? Image {get; set;}    
    public DateTime Timestamp {get; set;}
}

}