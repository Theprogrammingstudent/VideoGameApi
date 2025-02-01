using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using VideoGameApi.Data;

namespace VideoGameApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController(VideoGameDBContext context) : ControllerBase
    {

        private readonly VideoGameDBContext _context = context;

        [HttpGet]
        public async Task<ActionResult<List<VideoGame>>> GetVideoGames()
        {
            return Ok(await _context.VideoGames.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VideoGame>> GetVideoGameById(int id)
        {
            var game = await _context.VideoGames.FindAsync(id);
            if (game is null)
                return NotFound();

            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<VideoGame>> AddVideoGame(VideoGame newGame)
        {
            if (newGame is null)
                return BadRequest();


            _context.VideoGames.Add(newGame);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVideoGameById), new { id = newGame.Id }, newGame);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVideoGame(int id, VideoGame updatesGame)
        {
            var game = await _context.VideoGames.FindAsync(id);
            if (game is null)
                return NotFound();

            game.Title = updatesGame.Title;
            game.Platform = updatesGame.Platform;
            game.Developer = updatesGame.Developer;
            game.Publisher = updatesGame.Publisher;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVideoGame(int id)
        {
            var game = await _context.VideoGames.FindAsync(id);
            if (game is null)
                return NotFound(); ;

            _context.VideoGames.Remove(game);

            await _context.SaveChangesAsync();

            return NoContent();

        }

    }
}
