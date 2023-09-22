using Microsoft.AspNetCore.Mvc;
using Service.api.Movie.DBConfig;
using Service.api.Movie.Entities;
using Service.api.Movie.Repository;
using System.Collections.Generic;

namespace Service.api.Movie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly DBMoviePlusContext _context;
        private readonly MovieRepository _movieRepository;

        public MovieController(DBMoviePlusContext context)
        {
            _context = context;
            _movieRepository = new MovieRepository(_context);
        }

        [HttpPost]
        public async Task<ActionResult<ERequest<EMovie>>> Post(EMovie movie)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            ERequest<EMovie> response = await _movieRepository.CreateMovie(movie);
            return Created("", response);
        }

        [HttpPost("getMovieWithPagination")]
        public async Task<ActionResult<ERequest<List<EMovie>>>> GetMovieWithPagination(EPagination<List<EMovie>> pagination)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            ERequest<EPagination<List<EMovie>>> response = await _movieRepository.GetMovieWithPagination(pagination);
            return Ok(response);
        }
    }
}
