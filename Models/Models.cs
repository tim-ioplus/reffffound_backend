namespace API.Models;
public class Feedling
{
    public Findling Findling;
    public List<LinkedPost> LinkedPosts;
}

public class LinkedPost
{
    public string? Title;
    public string? Url;
    public string? ImageUrl;
    // public Bitmap? Image;
    public string? Image;
}

public class User 
{
    public int Id {get; set;}
    public string Name{ get; set;}
}