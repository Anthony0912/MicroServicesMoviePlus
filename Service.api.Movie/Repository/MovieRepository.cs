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
                var movieSort = await _context.Movie.OrderByDescending(o => o.Id).ToListAsync();
                if (pagination.SortDirection == "asc")
                {
                    movieSort = await _context.Movie.ToListAsync();
                }

                if (string.IsNullOrEmpty(pagination.Filter?.Value))
                {
                    pagination.Data = await _context.Movie
                        .Skip((pagination.Page - 1) * pagination.PageSize)
                        .Take(pagination.PageSize)
                        .ToListAsync();
                }
                else
                {
                    pagination.Data = await _context.Movie
                        .Where(w => w.Equals(pagination.Filter))
                        .Skip((pagination.Page - 1) * pagination.PageSize)
                        .Take(pagination.PageSize)
                        .ToListAsync();
                }

                var response = _request.Response(StatusCodes.Status200OK, pagination);
                return response;
            }
            catch (Exception e)
            {
                var error = _request.Response(StatusCodes.Status400BadRequest, "", e.Message);
                throw new Exception(error.ToString());
            }
        }
    }

}
