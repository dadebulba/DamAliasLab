using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VivaLaDama.Models
{
    public class GameSessionToSendSimplified
    {
        public long Id { get; }
        public string NamePlayer1 { get; }
        public string NamePlayer2 { get; }
        public GameSessionToSendSimplified(GameSession gameSession)
        {
            this.Id = gameSession.GameSessionId;
            this.NamePlayer1 = gameSession.NamePlayer1;
            this.NamePlayer2 = gameSession.NamePlayer2;
        }
    }
}
