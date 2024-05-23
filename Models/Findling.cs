using System.Globalization;

namespace API.Models;

public class Findling
{
    public int Id {get; set;}
    public string? Guid {get; set;}
    public string? Url {get; set;}
    public string? Title {get; set;}
    public string? Image {get; set;} 
    public string? Savedby {get; set;}
    public string? Timestamp {get; set;}
    public string? Context1link {get; set;}
    public string? Context1img{get; set;}
    public string? Context2link {get; set;}
    public string? Context2img {get; set;}
    public string? Context3link {get; set;}
    public string? Context3img{get; set;}
    public string? Usercontext {get; set;}
    public string? Furl{get; set;}
    
    /*
    @todo: add properties to testdata
    public int UserId {get; set;}
    public string? UserName{ get; set;}
    */

    public DateTime GetTimestamp()
    {
        string format = "yyyy-MM-dd HH:mm:ss";
        CultureInfo provider = new CultureInfo("de-de");
        const DateTimeStyles none = DateTimeStyles.None;

        DateTime dateTimeStamp = DateTime.Now;

        if (DateTime.TryParseExact(Timestamp, format, provider, none, out dateTimeStamp)){}
        
        return dateTimeStamp;
    }
}