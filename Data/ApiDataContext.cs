using Microsoft.EntityFrameworkCore;
using API.Models;
using System.Globalization;
using System.Text.Json;

namespace API.Data;

public class ApiDataContext : DbContext
{
    public DbSet<Findling> Findlings {get; set;}
    public DbSet<User> Users {get; set;}
    public string ContentRootPath { get; set; }

    private bool _hydrated = false;
    public ApiDataContext(DbContextOptions<ApiDataContext> options) : base(options)
    {
        _hydrated = false;
        this.ContentRootPath = "";
    }

    public bool Hydrate()
    {
        if(this.Users.Any() && this.Findlings.Any()) return true;

        var usersMockDataFilePath = Path.Combine(ContentRootPath,"Data/users.json");
        if(File.Exists(usersMockDataFilePath))
        {
            string usersText = File.ReadAllText(usersMockDataFilePath);
            if(string.IsNullOrWhiteSpace(usersText)) return false;
            
            var users = JsonSerializer.Deserialize<List<User>>(usersText);
            if(users != null && users.Count > 0)
            {
                users.ForEach(user => this.Users.Add(user));
                this.SaveChanges();
            }
            else
            {
                return false;
            }
        }

        var findlingsMockDataFilePath = Path.Combine(ContentRootPath,"Data/findlings.json");
        if(File.Exists(findlingsMockDataFilePath))
        {
            string findlingsText = File.ReadAllText(findlingsMockDataFilePath);
            if(string.IsNullOrWhiteSpace(findlingsText)) return false;

            var findlings = JsonSerializer.Deserialize<List<Findling>>(findlingsText);
            if(findlings != null && findlings.Count > 0)
            {
                findlings.ForEach(findling => this.Findlings.Add(findling));
                this.SaveChanges();
            }
            else
            {
                return false;
            }
        }        

        this.Findlings.OrderBy(f => f.Id);

        _hydrated = this.Users.Count() >= 5 && this.Findlings.Count() >= 20;
        return _hydrated;
    } 
}