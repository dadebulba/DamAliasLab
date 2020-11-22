using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VivaLaDama.Models
{
    public class PawnPositioned : Pawn
    {
        public Coordinate Position { get; }
        public PawnPositioned(ColorPawn color, long id, Coordinate position) : base(color, id)
        {
            this.Position = position;
        }
    }
}
