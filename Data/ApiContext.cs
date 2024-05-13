using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Data;

public class ApiContext : DbContext
{
    public DbSet<Findling> Findlings {get; set;}
    public DbSet<User> Users {get; set;}
    public ApiContext(DbContextOptions<ApiContext> options) : base(options)
    {
    }

    public bool Hydrate()
    {
        bool hydrated = false;
        
        var user1 = new User();
        user1.Name = "ioplus";
        user1.EMail = "ip1@ioplus.de";
        user1.Password = "testpw1";
        user1.Active = false;

        var user2 = new User();
        user2.Name = "nerdcoreRIP";
        user2.EMail = "piffpaff1245@gmail.com";
        user2.Password = "testpw2";
        user2.Active = false;

        var us1 = this.Users.Add(user1);
        user1.Id = us1.Entity.Id;
        var us2 = this.Users.Add(user2);
        user2.Id = us2.Entity.Id;

        this.SaveChanges();

        var findling1 = new Findling();
        findling1.Guid = Guid.NewGuid().ToString();
        findling1.UserId = user1.Id;
        findling1.UserName = user1.Name;
        findling1.Url= "https://i.imgur.com/rGXMMih";
        findling1.Title = "Space loop 3000";
        findling1.Image = "https://i.imgur.com/rGXMMih.jpeg";

              
        var findling2 = new Findling();
        findling2.Guid = Guid.NewGuid().ToString();
        findling2.UserId = user1.Id;
        findling2.UserName = user1.Name;
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