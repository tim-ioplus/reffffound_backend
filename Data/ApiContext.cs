using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Data;

public class ApiContext : DbContext
{
    public DbSet<Findling> Findlings {get; set;}
    public ApiContext(DbContextOptions<ApiContext> options) : base(options)
    {
    }

    public bool Hydrate()
    {
        bool hydrated = false;
        
        var user = new User();
        user.Id = 1;
        user.Name = "ioplus";

        var findling1 = new Findling();
        findling1.Guid = Guid.NewGuid().ToString();
        findling1.UserId = user.Id;
        findling1.UserName = user.Name;
        findling1.Url= "https://i.imgur.com/rGXMMih";
        findling1.Title = "Space loop 3000";
        findling1.Image = "https://i.imgur.com/rGXMMih.jpeg";

              
        var findling2 = new Findling();
        findling2.Guid = Guid.NewGuid().ToString();
        findling2.UserId = user.Id;
        findling2.UserName = user.Name;
        findling2.Url= "https://i.imgur.com/r0LmBKH";
        findling2.Title = "Space loop 3000";
        findling2.Image = "https://i.imgur.com/r0LmBKH.jpeg";
        
        var f1x = this.Findlings.Add(findling1);
        var f2x = this.Findlings.Add(findling2);
        this.SaveChanges();

        hydrated = this.Findlings.Count() == 2;

        return hydrated;
    }
    
}