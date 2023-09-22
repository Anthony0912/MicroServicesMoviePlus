namespace Service.api.Movie.Entities
{
    public class ERequest<T>
    {
        public T? Response { get; set; }

        public int StatusCode { get; set; }

        public string? TraceInformation { get; set; }

        public string? CurrentException { get; set; }

    }
}
