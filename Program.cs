using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Auth;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "corsPolicy",
                      policy  =>
                      {
                          policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost:4200/")
                          .WithOrigins("http://localhost:4200")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                      });
});


// Add services to the container.
builder.Services.AddDbContext<ApiDataContext>(options => options.UseInMemoryDatabase("Reffffound_ApiDataDb"));
builder.Services.AddDbContext<ApiAuthContext>(options => options.UseInMemoryDatabase("Reffffound_ApiAuthDb"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication();


builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<ApiAuthContext>();
    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapIdentityApi<IdentityUser>();
app.MapControllers();

app.Run();
