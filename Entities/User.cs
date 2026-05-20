using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Lab_Results.Entities
{
    public class User : IdentityUser
    {
        public string? Firstname { get; set; }
        public string? LastName { get; set; }


    }
}
