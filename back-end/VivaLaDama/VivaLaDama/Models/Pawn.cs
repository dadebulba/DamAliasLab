using System;

namespace VivaLaDama.Models
{
    public class Pawn
    {
        public enum ColorPawn { WHITE, BLACK }
        public long Id { get; }
        public ColorPawn Color { get; }
        public bool Upgraded { get; set; }
        public Pawn()
        {
            this.Id = -131;
            this.Color = ColorPawn.WHITE;
            this.Upgraded = false;
        }
        public Pawn(ColorPawn color, long id)
        {
            this.Color = color;
            this.Id = id;
            this.Upgraded = false;
        }
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
                ret = this.Id==pawn.Id && this.Color==pawn.Color;
            }

            return ret;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
