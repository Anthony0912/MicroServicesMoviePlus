namespace Service.api.Movie.Entities
{
    public class EPagination<T>
    {
        public int PageSize { get; set; }
        public int Page { get; set; }
        public string SortDirection { get; set; } = "desc";
        public EFilter? Filter { get; set; }
        public int PagesQuantity { get; set; }
        public int? TotalRows { get; set; }
        public T? Data { get; set; }
    }
}
