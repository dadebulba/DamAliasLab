using System.Collections.Generic;

namespace VivaLaDama.Models
{
    public class GameSession
    {
        public long GameSessionId { get; set; }
        public string NamePlayer1 { get; set; }
        public string NamePlayer2 { get; set; }
        public Chessboard Game { get; set; }
        public List<Move> Moves { get; set; }
        public bool ExecuteMove(Move move, bool insertMove, bool changeTarget)
        {
            bool ret = this.Game.ExecuteMove(move, changeTarget);

            if(ret==true && insertMove==true)
            {
                move.GameSessionId = this.GameSessionId;
                this.Moves.Add(move);
            }

            return ret;
        }
    }
}
