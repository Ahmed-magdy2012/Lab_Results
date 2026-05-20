using Lab_Results.Data;
using Lab_Results.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddIdentityApiEndpoints<User>().AddRoles<IdentityRole>().
    AddEntityFrameworkStores<MyDatabase>();
builder.Services.AddControllers();
builder.Services.AddDbContext<MyDatabase>(options => options.UseSqlServer(
builder.Configuration.GetConnectionString("Default")
    ));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSession();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapIdentityApi<User>();

app.MapControllers();
var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;
var context = services.GetRequiredService<MyDatabase>();
var userManager = services.GetRequiredService<UserManager<User>>();
await seeding.seed(context, userManager);
app.Run();
