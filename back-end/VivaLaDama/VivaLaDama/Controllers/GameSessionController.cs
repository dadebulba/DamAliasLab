using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            GameSession game = this.RetrieveGameSession(id);

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
            GameSession game;

            if (names.NamePlayer1 == null || names.NamePlayer2 == null)
            {
                return StatusCode(400);
            }

            if (names.NamePlayer1.All(car => char.IsWhiteSpace(car)) || names.NamePlayer2.All(car => char.IsWhiteSpace(car)))
            {
                return StatusCode(400);
            }

            game = new GameSession { Game = new Chessboard(), Moves = new List<Move>(), NamePlayer1=names.NamePlayer1, NamePlayer2=names.NamePlayer2 };

            this._context.Set<GameSession>().Add(game);
            this._context.SaveChanges();

            return CreatedAtAction(nameof(GetGameSession), new { id = game.GameSessionId }, new GameSessionToSend(game));
        }
        //PUT api/game/131
        [HttpPut("{id}")]
        public async Task<ActionResult<GameSessionToSend>> PutGameSession(long id, Move move)
        {
            GameSession game = this.RetrieveGameSession(id);

            if (game == null)
            {
                return NotFound();
            }

            if (game.ExecuteMove(move, true, true))
            {
                try
                {
                    this._context.SaveChanges();
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
        //DELETE api/game/131
        [HttpDelete("{id}")]
        public async Task<ActionResult<GameSessionToSend>> DeleteGameSession(long id)
        {
            GameSession game = this.RetrieveGameSession(id);

            if (game == null)
            {
                return NotFound();
            }

            this._context.Remove(game);
            await this._context.SaveChangesAsync();

            return new GameSessionToSend(game);
        }


        //DELETE api/game/123/lastMove
        [HttpDelete("{id}/lastMove")]
        public async Task<ActionResult<GameSessionToSend>> DeleteLastMove(long id)
        {
            GameSession game = this._context.GameSessions.Find(id);

            if (game == null)
            {
                return NotFound();
            }

            if (DeleteLastMoveFromDb(game))
            {
                return new GameSessionToSend(game);
            }
            else
            {
                return BadRequest();
            }
        }

        public bool DeleteLastMoveFromDb(GameSession game)
        {
            if (game.Moves.Count > 0)
            {
                bool isRemoved = false;
                Move lastMove = game.Moves.Last();
                isRemoved = game.Moves.Remove(lastMove);
                _context.GameSessions.Update(game);
                if (_context.SaveChanges() == 1 && isRemoved)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }

        }

        public bool GameSessionExists(long id)
        {
            return this._context.GameSessions.Any(game => game.GameSessionId == id);
        }
        public GameSession RetrieveGameSession(long id)
        {
            GameSession game = this._context.GameSessions.Where(game => game.GameSessionId == id).Include(game => game.Moves).FirstOrDefault();

            if (game != null)
            {
                game.Game = new Chessboard();

                for (int i = 0; i < game.Moves.Count; i++)
                {
                    game.ExecuteMove(game.Moves[i], false, false);
                }
            }

            return game;
        }
    }
}
