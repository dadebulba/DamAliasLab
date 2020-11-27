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
        //GET api/game/131
        [HttpGet("{id}")]
        public async Task<ActionResult<GameSessionToSend>> GetGameSession(long id)
        {
            GameSession game = (await this.RetrieveGameSession(id)).Value;

            if (game == null)
            {
                return NotFound();
            }

            return new GameSessionToSend(game);
        }
        //POST api/game
        [HttpPost]
        public async Task<ActionResult<IEnumerable<GameSessionToSend>>> PostGameSessions(NameOfPlayers names)
        {
            GameSession game = new GameSession();
            game.NamePlayer1 = names.NamePlayer1;
            game.NamePlayer2 = names.NamePlayer2;

            this._context.Add(game);
            await this._context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGameSessions), new { id = game.IdGame }, new GameSessionToSend(game));
        }
        //PUT api/game/131
        [HttpPut("{id}")]
        public async Task<ActionResult<GameSessionToSend>> PutGameSession(long id, MoveToRecv move)
        {
            GameSession game = (await this.RetrieveGameSession(id)).Value;

            if(game==null)
            {
                return NotFound();
            }

            Console.WriteLine("--------------------PUT--------------------");
            Console.WriteLine("move.Target = idTarget:{0}, colorTarget:{1}", move.Target.Id, move.Target.Color);
            Console.WriteLine("move.To = ({0},{1})", move.To.Row, move.To.Column);

            if (/*game.ExecuteMove(move)*/false)
            {
                try
                {
                    await this._context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.GameSessionExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                return StatusCode(403);
            }

            return new GameSessionToSend(game);
        }
        private bool GameSessionExists(long id)
        {
            return this._context.GameSessions.Any(game => game.IdGame == id);
        }
        private async Task<ActionResult<GameSession>> RetrieveGameSession(long id)
        {
            GameSession game = await this._context.GameSessions.FindAsync(id);

            return game;
        }
    }
}
