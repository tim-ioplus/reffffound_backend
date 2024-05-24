using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Auth;
public class ApiAuthContext : IdentityDbContext<IdentityUser>
{
    public ApiAuthContext(DbContextOptions<ApiAuthContext> options) :
        base(options)
    { }
}