using System.Collections.Generic;

namespace VivaLaDama.Models
{
    public class GameSession
    {
        public long IdGame { get; set; }
        public string NamePlayer1 { get; set; }
        public string NamePlayer2 { get; set; }
        public Chessboard Game { get; }
        public List<Move> Moves { get; }
        private int numMosse;
        public GameSession()
        {
            this.Game = new Chessboard();
            this.Moves = new List<Move>();
            this.numMosse = 0;
        }

        public GameSession(string player1, string player2)
        {
            this.Game = new Chessboard();
            this.Moves = new List<Move>();
            this.numMosse = 0;
            this.NamePlayer1 = player1;
            this.NamePlayer2 = player2;

        }
        public bool ExecuteMove(Move move)
        {
            bool ret = this.Game.ExecuteMove(move);

            if (ret == true)
            {
                move.IdMossa = this.numMosse++;
                this.Moves.Add(move);
            }

            return ret;
        }
    }
}
