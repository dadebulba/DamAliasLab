using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VivaLaDama.Models
{
    public class GameSessionToSendSimplified
    {
        public int Id { get; }
        public string NamePlayer1 { get; }
        public string NamePlayer2 { get; }
        public GameSessionToSendSimplified(int id, string namePlayer1, string namePlayer2)
        {
            this.Id = id;
            this.NamePlayer1 = namePlayer1;
            this.NamePlayer2 = namePlayer2;
        }
    }
}
