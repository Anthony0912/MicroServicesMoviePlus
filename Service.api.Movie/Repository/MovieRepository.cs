using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Service.api.Movie.DBConfig;
using Service.api.Movie.Entities;
using Service.api.Movie.HandleErrors;

namespace Service.api.Movie.Repository
{
    public class MovieRepository
    {
        private readonly DBMoviePlusContext _context;
        private readonly Request _request;

        public MovieRepository(DBMoviePlusContext context)
        {
            _context = context;
            _request = new Request();
        }

        public async Task<ERequest<EMovie>> CreateMovie(EMovie movie)
        {
            try
            {
                _context.Movie.Add(movie);
                await _context.SaveChangesAsync();
                var response = _request.Response(StatusCodes.Status201Created, movie);
                return response;
            }
            catch (Exception e)
            {
                var error = _request.Response(StatusCodes.Status400BadRequest, "", e.Message);
                throw new Exception(error.ToString());
            }
        }

        public async Task<ERequest<EPagination<List<EMovie>>>> GetMovieWithPagination(EPagination<List<EMovie>> pagination)
        {
            try
            {
                string sql = string.Format(
                    "EXEC dbo.Pagination " +
                    "@TableName = '{0}', " +
                    "@PageSize = {1}, " +
                    "@Page = {2}, " +
                    "@SortDirection = '{3}', " +
                    "@PropertySortDirection = '{4}', " +
                    "@ValueFilter = '{5}';",
                    "Movies",
                    pagination.PageSize,
                    pagination.Page,
                    pagination.SortDirection,
                    pagination.PropertySortDirection,
                    pagination.ValueFilter
                );

                List<EMovie> Movies = await _context.Movie.FromSqlRaw(sql).ToListAsync();

                pagination.Data = Movies;

                pagination = await GetQuantityItemsInTable(pagination);

                var response = _request.Response(StatusCodes.Status201Created, pagination);
                return response;

            }
            catch (Exception e)
            {
                var error = _request.Response(StatusCodes.Status400BadRequest, "", e.Message);
                throw new Exception(error.ToString());
            }
        }

        protected async Task<EPagination<List<EMovie>>> GetQuantityItemsInTable(EPagination<List<EMovie>> pagination)
        {
            try
            {
                var response = await _context
                     .Database
                     .SqlQueryRaw<int>(
                      "EXEC dbo.QuantityItemsInTable @TableName, @PropertySortDirection, @ValueFilter;",
                      new SqlParameter("TableName", "Movies"),
                      new SqlParameter("PropertySortDirection", pagination.PropertySortDirection),
                      new SqlParameter("ValueFilter", pagination.ValueFilter)
                    ).ToListAsync();

                int totalRows = response[0];
                decimal rounded = Math.Ceiling(totalRows / Convert.ToDecimal(pagination.PageSize));
                int totalPages = Convert.ToInt32(rounded);

                pagination.PagesQuantity = totalPages;
                pagination.TotalRows = totalRows;


                return pagination;
            }
            catch (Exception e)
            {
                var error = _request.Response(StatusCodes.Status400BadRequest, "", e.Message);
                throw new Exception(error.ToString());
            }
        }
    }

}
