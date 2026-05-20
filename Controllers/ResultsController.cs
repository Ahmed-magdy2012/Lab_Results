using Lab_Results.Data;
using Lab_Results.Entities.NewFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Lab_Results.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController(MyDatabase _context) : ControllerBase
    {

        [HttpGet("accessLogin")]
        public async Task<IActionResult> accessLogin([FromQuery] string sid,
             [FromQuery] string language)
        {
            var patient = await _context.Patients
             .Include(x => x.Results)
             .FirstOrDefaultAsync(x => x.Sid == sid);

            patient.Language = language;
            if (patient == null)
                return NotFound("Invalid or expired link");
            if (patient.ExpiryDate < DateTime.UtcNow)
                return Unauthorized("Link expired");


            HttpContext.Session.SetString("SID", sid);

            return Ok(new
            {
                patient.PatientNo,
                patient.PatientName,
                patient.MobileNumber,
                patient.DateOfBirth,
                patient.Gender,
                patient.ExpiryDate,
                patient.Language,
                results = patient.Results.Select(r => new
                {
                    r.ResultValue,
                    r.IsNormal,
                    r.CreatedAt,
                    r.PdfPath
                })
              
            });


        }

    

    }

}
