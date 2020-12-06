using System;

namespace VivaLaDama.Models
{
    public class Pawn
    {
        public enum ColorPawn { WHITE, BLACK }
        public long PawnId { get; set; }
        public ColorPawn Color { get; set; }
        public bool Upgraded { get; set; }
        public ColorPawn GetOpponentColor()
        {
            return (this.Color == ColorPawn.WHITE ? ColorPawn.BLACK : ColorPawn.WHITE);
        }
        public bool IsColorValid()
        {
            return this.Color == ColorPawn.WHITE || this.Color == ColorPawn.BLACK;
        }
        public override bool Equals(Object obj)
        {
            bool ret;

            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                ret = false;
            }
            else
            {
                Pawn pawn = (Pawn)obj;
                ret = this.PawnId == pawn.PawnId && this.Color==pawn.Color;
            }

            return ret;
        }
    }
}
