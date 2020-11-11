using System;

namespace VivaLaDama.Models
{
    public class Pedina
    {
        public enum ColorePedina { WHITE, BLACK, NONE }
        public long Id { get; }
        public ColorePedina Color { get; }
        public bool Upgraded { get; set; }
        public Pedina(ColorePedina color, long id)
        {
            this.Color = color;
            this.Id = id;
            this.Upgraded = false;
        }
        public ColorePedina GetColoreOpponente()
        {
            if(Color==ColorePedina.NONE)
            {
                return Color;
            }
            return (Color == ColorePedina.WHITE ? ColorePedina.BLACK : ColorePedina.WHITE);
        }
        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            Pedina pedina = (Pedina)obj;
            return this.Id==pedina.Id && this.Color==pedina.Color;
        }
    }
}
