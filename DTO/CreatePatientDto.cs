using Lab_Results.Entities;
using System.ComponentModel.DataAnnotations;

namespace Lab_Results.DTO
{
    public class CreatePatientDto
    {

        public string PatientNo { get; set; } = default!;

        public string? MobileNumber { get; set; }

        public string PatientName { get; set; } = default!;

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public string Language { get; set; } = "en";
        public List<ResultDto> Results { get; set; } = new();
    }
}
