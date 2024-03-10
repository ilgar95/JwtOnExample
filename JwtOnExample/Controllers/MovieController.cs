using JwtOnExample.ActionFilters;
using JwtOnExample.Core.DbContext;
using JwtOnExample.Entities;
using JwtOnExample.Extentions;
using Microsoft.AspNetCore.Mvc;

namespace JwtOnExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MovieController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var movies = _context.Movies.ToList();

            return Ok(movies);
        }

        [HttpGet("{id}", Name = "MovieById")]
        public IActionResult Get(Guid id)
        {
            var dbMovie = HttpContext.Items["entity"] as Movie;

            return Ok(dbMovie);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult Post([FromBody] Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();

            return CreatedAtRoute("MovieById", new { id = movie.Id }, movie);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult Put(Guid id, [FromBody] Movie movie)
        {
            var dbMovie = HttpContext.Items["entity"] as Movie;

            dbMovie.Map(movie);

            _context.Movies.Update(dbMovie);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var dbMovie = HttpContext.Items["entity"] as Movie;

            _context.Movies.Remove(dbMovie);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
