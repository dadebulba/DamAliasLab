using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VivaLaDama.Models
{
    public class GiocoDama
    {
        public long IdPartita { get; set; }
        public string NomePlayer1 { get; set; }
        public string NomePlayer2 { get; set; }
        public CampoDiGioco Scacchiera { get; set; }
        public Mossa[] CronologiaMosseValide { get; set;  }
    }
}
