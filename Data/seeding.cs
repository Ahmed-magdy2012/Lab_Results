using Lab_Results.Entities;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using System.Text.Json;

namespace Lab_Results.Data
{
    public class seeding
    {
        public static async Task seed(MyDatabase context, UserManager<User> usermanager)
        {
            if (!context.Users.Any())
            {
                context.Users.Add(new User
                {
                    Id = "fixed-id",
                    UserName = "admin"
                });

                await context.SaveChangesAsync();
            }
            if (!usermanager.Users.Any(x => x.UserName == "admin@test.com"))
            {
                var user = new User
                {
                    UserName = "admin@test.com",
                    Email = "admin@test.com"
                };
                await usermanager.CreateAsync(user, "Pa$$w0rd");
                await usermanager.AddToRoleAsync(user, "Admin");

            }
            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Data",
                "Results.json"
            ); if (!context.Results.Any())
            {
                var RES = await File.ReadAllTextAsync(filePath);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var Result = JsonSerializer.Deserialize<List<Patient>>(RES,options);
                context.Patients.AddRange(Result);

                await context.SaveChangesAsync();
            }

        }
    }
}
