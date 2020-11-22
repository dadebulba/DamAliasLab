﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VivaLaDama.Models
{
    public class PawnPositioned : Pawn
    {
        public Coordinate Position { get; }
        public PawnPositioned(Pawn pawn, Coordinate position) : base(pawn.Color, pawn.Id)
        {
            this.Upgraded = pawn.Upgraded;
            this.Position = position;
        }
    }
}
