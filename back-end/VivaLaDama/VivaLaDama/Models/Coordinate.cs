using Microsoft.EntityFrameworkCore;

namespace VivaLaDama.Models
{
    public class Coordinate
    {
        public int Row { get; }
        public int Column { get; }
        public Coordinate()
        {
            this.Row = -1;
            this.Column = -1;
        }
        public Coordinate(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }
        public static Coordinate operator -(Coordinate coord1, Coordinate coord2)
        {
            return new Coordinate(coord1.Row - coord2.Row, coord1.Column - coord2.Column);
        }
        public override bool Equals(object obj)
        {
            bool ret;

            if(obj==null || !this.GetType().Equals(obj.GetType()))
            {
                ret = false;
            }
            else
            {
                Coordinate coordinate = (Coordinate)obj;
                ret = this.Row==coordinate.Row && this.Column==coordinate.Column;
            }

            return ret;
        }
        public Coordinate GetDownLeft()
        {
            return new Coordinate(this.Row + 1, this.Column - 1);
        }
        public Coordinate GetDownRight()
        {
            return new Coordinate(this.Row + 1, this.Column + 1);
        }
        public Coordinate GetUpLeft()
        {
            return new Coordinate(this.Row - 1, this.Column - 1);
        }
        public Coordinate GetUpRight()
        {
            return new Coordinate(this.Row - 1, this.Column + 1);
        }
        public bool IsValid(long maxValue)
        {
            return this.Row >= 0 && this.Column >= 0 && this.Row < maxValue && this.Column < maxValue;
        }
    }
}
