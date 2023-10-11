namespace TestTask.Data.Entitys
{
    public class PageInformation
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }

        public PageInformation(int pageNumber, int pageSize, int totalItems)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.TotalItems = totalItems;
        }
    }
}
