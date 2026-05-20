using Lab_Results.Data;
using Lab_Results.DTO;
using Lab_Results.Entities;
using Lab_Results.Entities.NewFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Lab_Results.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class admin(SignInManager<User> _signIn,MyDatabase _context): ControllerBase
    {
       

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _signIn.UserManager.FindByEmailAsync(model.Email);

            if (user == null)
                return Unauthorized();

            var result = await _signIn
                .CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
                return Unauthorized();

            var roles = await _signIn.UserManager.GetRolesAsync(user);


            return Ok(new
            {
                email = user.Email,
                r = roles
            });
        }



        [HttpPost("patients")]
        public async Task<string> CreateResultSession(CreatePatientDto dto)
        {
            var sid = Guid.NewGuid().ToString("N");

            var Patient = new Patient
            {
                Language=dto.Language,
                PatientName = dto.PatientName,
                PatientNo = dto.PatientNo,
                DateOfBirth = dto.DateOfBirth,
                MobileNumber=dto.MobileNumber,
                Gender = dto.Gender,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                Results = dto.Results
        .Select(r => new Result
        {
            ResultValue = r.ResultValue,
            IsNormal = r.IsNormal,
            CreatedAt = r.CreatedAt,
            PdfPath = r.PdfPath
        })
        .ToList()

            };

            _context.Patients.Add(Patient);
            await _context.SaveChangesAsync();
            var link = new  AccessLink
            {
                Code = Guid.NewGuid().ToString("N"),
                Sid = Patient.Sid

            };

            _context.Links.Add(link);
            await _context.SaveChangesAsync();
            return Patient.Sid;
        }



        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await _signIn.SignOutAsync();
            return Ok();
        }



        [HttpGet("My_patients")]

        public async Task<ActionResult> GetAllpatients( [FromQuery] QueryParams param)
        {
            var query = _context.Patients.Include(x => x.Results) .AsQueryable();

            if (!string.IsNullOrEmpty(param.PatientNo))
                query = query.Where(p => p.PatientNo == param.PatientNo);

            if (!string.IsNullOrEmpty(param.PatientName))
                query = query.Where(p => p.PatientName.Contains(param.PatientName));

            if (!string.IsNullOrEmpty(param.MobileNumber))
                query = query.Where(p => p.MobileNumber == param.MobileNumber);

            if (param.IsNormal.HasValue)
            { 
            
              query = query.Where(x =>
                    x.Results.Any(r => r.IsNormal == param.IsNormal)
                );
            }
              
            var totalCount = await query.CountAsync();

            var patients = await query
          .Skip((param.Pageindex - 1) * param.Pagesize)
          .Take(param.Pagesize)
          .Select(p => new ResultSpecParams
          {
              PatientNo = p.PatientNo,
              PatientName = p.PatientName,
              MobileNumber = p.MobileNumber,

              Results = p.Results.Select(r => new ResultDto
              {
                  ResultValue=r.ResultValue,
                  CreatedAt=r.CreatedAt,
                  PdfPath=r.PdfPath,
                  IsNormal = r.IsNormal,

              }).ToList()
          })
          .ToListAsync();

            return Ok(new
            {
                PageIndex = param.Pageindex,
                PageSize = param.Pagesize,
                Count = totalCount,
                Data = patients
            });
        }
    }



    }

   

