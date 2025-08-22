namespace CCC_Rugby_Web.DTOs
{
    public class PaginacionDTO
    {
        public int? TotalItems { get; set; } = null;
        public bool GetTotalItems { get; set; } = false;
        private int page;
        public int Page
        {
            get => page <= 0 ? 1 : page;
            set => page = value;
        }
        private int recordsPerPage;
        public int RecordsPerPage
        {
            get => recordsPerPage <= 0 ? 10 : recordsPerPage > 50 ? 50 : recordsPerPage;
            set => recordsPerPage = value;
        }

    }
}
