using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VivaLaDama.Models;

namespace VivaLaDama.Controllers
{
    [Route("api/game")]
    [ApiController]
    public class GameSessionController : ControllerBase
    {
        private readonly GameSessionContext _context;

        public GameSessionController(GameSessionContext context)
        {
            this._context = context;
        }

        //GET api/game
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameSessionToSendSimplified>>> GetGameSessions()
        {
            return await _context.GameSessions.Select(x => new GameSessionToSendSimplified(x)).ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GameSessionToSendSimplified>> GetGameSession(long id)
        {
            GameSession game = await this._context.GameSessions.FindAsync(id);

            if(game==null)
            {
                return NotFound();
            }

            return new GameSessionToSendSimplified(game);
        }
        //POST api/game
        [HttpPost]
        public async Task<ActionResult<IEnumerable<NameOfPlayers>>> PostGameSessions(NameOfPlayers names)
        {
            GameSession game = new GameSession();
            game.NamePlayer1 = names.NamePlayer1;
            game.NamePlayer2 = names.NamePlayer2;

            this._context.Add(game);
            await this._context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGameSessions), new { id = game.IdGame }, new GameSessionToSend(game));
        }
    }
}
