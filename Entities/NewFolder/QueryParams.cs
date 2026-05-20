namespace Lab_Results.Entities.NewFolder
{
    public class QueryParams
    {
        private const int Maxpagesize = 50;
        public int Pageindex { get; set; } = 1;
        private int _Pagesize = 6;
        public int Pagesize
        {
            get => _Pagesize;
            set => _Pagesize = value > Maxpagesize ? Maxpagesize : value;
        }


        public string? PatientNo { get; set; }
        public string? PatientName { get; set; }
        public string? MobileNumber { get; set; }
        public bool? IsNormal { get; set; }


    }
}
