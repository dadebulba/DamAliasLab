using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VivaLaDama.Models
{
    public class GameSessionToSend
    {
        public long Id { get; }
        public string NamePlayer1 { get; }
        public string NamePlayer2 { get; }
        public List<Move> Moves { get; }
        public List<PawnPositioned> Black { get; }
        public List<PawnPositioned> White { get; }
        public Pawn.ColorPawn Turn { get; }
        public int PointsWhite { get; }
        public int PointsBlack { get; }
        public GameSessionToSend(GameSession gameSession)
        {
            this.Id = gameSession.GameSessionId;
            this.NamePlayer1 = gameSession.NamePlayer1;
            this.NamePlayer2 = gameSession.NamePlayer2;
            this.Turn = gameSession.Game.GetTurn();
            this.PointsWhite = gameSession.Game.PointsWhite;
            this.PointsBlack = gameSession.Game.PointsBlack;
            this.Moves = gameSession.Moves;
            this.Black = gameSession.Game.GetBlack();
            this.White = gameSession.Game.GetWhite();
        }
    }
}
