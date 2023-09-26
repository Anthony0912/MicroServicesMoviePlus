namespace Service.api.Movie.Entities
{
    public class EPagination<T>
    {

        public int PageSize { get; set; }

        public int Page { get; set; }

        public string SortDirection { get; set; } = "ASC";

        public string PropertySortDirection { get; set; } = "";

        public string ValueFilter { get; set; } = "";

        public int PagesQuantity { get; set; }

        public int? TotalRows { get; set; }

        public T? Data { get; set; }
    }
}
