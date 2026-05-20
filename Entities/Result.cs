
namespace Lab_Results.Entities
{
    public class Result :BaseEntity
    {

        public decimal ResultValue { get; set; }

        public bool IsNormal { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? PdfPath { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; } = default!;
    }
}