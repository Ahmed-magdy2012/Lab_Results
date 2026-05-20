using Lab_Results.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab_Results.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedirectController(MyDatabase myDatabase) : ControllerBase
    {
        [HttpGet("{code}")]
        public async Task<IActionResult> Open(string code,string lang="en")
        {
            var link = await myDatabase.Links
                .FirstOrDefaultAsync(x => x.Code == code);
        
            if (link == null)
                return NotFound();

            return Redirect(
                $"https://localhost:7110/api/Results/accessLogin?sid={link.Sid}&language={lang}");
        }
    }
}
