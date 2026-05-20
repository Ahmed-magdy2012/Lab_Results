namespace Lab_Results.DTO
{
    public class ResultSpecParams
    {

        public string? PatientNo { get; set; }
        public string? PatientName { get; set; }
        public string? MobileNumber { get; set; }

        public List<ResultDto>? Results { get; set; }
    }

    public class ResultDto
    {

        public decimal ResultValue { get; set; }

        public bool IsNormal { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? PdfPath { get; set; }

    }
}