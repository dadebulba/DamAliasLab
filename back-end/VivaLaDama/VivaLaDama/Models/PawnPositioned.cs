using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VivaLaDama.Models
{
    public class PawnPositioned : Pawn
    {
        public Coordinate Position { get; }
        public PawnPositioned(Pawn pawn, Coordinate position) : base()
        {
            this.PawnId = pawn.PawnId;
            this.Color = pawn.Color;
            this.Upgraded = pawn.Upgraded;
            this.Position = new Coordinate { Row = position.Row, Column = position.Column };
        }
    }
}
