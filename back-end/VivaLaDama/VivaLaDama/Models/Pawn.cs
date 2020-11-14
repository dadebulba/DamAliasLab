using System;

namespace VivaLaDama.Models
{
    public class Pawn
    {
        public enum ColorPawn { WHITE, BLACK }
        public long Id { get; }
        public ColorPawn Color { get; }
        public bool Upgraded { get; set; }
        public Pawn(ColorPawn color, long id)
        {
            this.Color = color;
            this.Id = id;
            this.Upgraded = false;
        }
        public ColorPawn GetOpponentColor()
        {
            if(this.Color!=ColorPawn.WHITE && this.Color !=ColorPawn.BLACK)
            {
                return this.Color;
            }
            return (this.Color == ColorPawn.WHITE ? ColorPawn.BLACK : ColorPawn.WHITE);
        }
        public bool IsColorValid()
        {
            return this.Color == ColorPawn.WHITE || this.Color == ColorPawn.BLACK;
        }
        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            Pawn pawn = (Pawn)obj;
            return this.Id==pawn.Id && this.Color==pawn.Color;
        }
    }
}
