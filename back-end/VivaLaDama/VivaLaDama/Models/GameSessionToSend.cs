using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VivaLaDama.Models
{
    public class GameSessionToSend
    {
        public long Id { get; set; }
        public string NamePlayer1 { get; set; }
        public string NamePlayer2 { get; set; }
        public List<Move> Moves { get; }
        public List<PawnPositioned> Black { get; }
        public List<PawnPositioned> White { get; }
        public GameSessionToSend(GameSession gameSession)
        {
            this.Id = gameSession.IdGame;
            this.NamePlayer1 = gameSession.NamePlayer1;
            this.NamePlayer2 = gameSession.NamePlayer2;
            this.Moves = gameSession.Moves;
            this.Black = gameSession.Game.GetBlack();
            this.White = gameSession.Game.GetWhite();
        }
    }
}
