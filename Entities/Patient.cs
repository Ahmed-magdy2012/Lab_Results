using System.Text.Json.Serialization;

namespace Lab_Results.Entities
{
    public class Patient :BaseEntity
    {

        public string PatientNo { get; set; } = default!;
        public string? MobileNumber { get; set; } = default!;

        public string PatientName { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; } = default!;
        public DateTime ExpiryDate { get; set; }
        public DateTime VisitDate { get; set; } = DateTime.UtcNow;

        public ICollection<Result> Results { get; set; } = new List<Result>();
        public string Sid { get; set; } = Guid.NewGuid().ToString(("N"));
        public string Language { get; set; } = "en";


    }
}
