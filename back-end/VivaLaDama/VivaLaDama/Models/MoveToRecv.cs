using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VivaLaDama.Models
{
    public class MoveToRecv
    {
        public PawnToRecv Target { get; set; }
        public Coordinate To { get; set; }
    }
}
