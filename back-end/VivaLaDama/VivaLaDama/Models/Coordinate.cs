namespace VivaLaDama.Models
{
    public class Coordinate
    {
        public int Riga { get; set; }
        public int Colonna { get; set; }
        public Coordinate(int riga, int colonna)
        {
            this.Riga = riga;
            this.Colonna = colonna;
        }
        public override bool Equals(object obj)
        {
            if(obj==null || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            Coordinate c = (Coordinate)obj;
            return this.Riga == c.Riga && this.Colonna == c.Colonna;
        }
        public Coordinate GetDownLeft()
        {
            return new Coordinate(Riga + 1, Colonna - 1);
        }
        public Coordinate GetDownRight()
        {
            return new Coordinate(Riga + 1, Colonna + 1);
        }
        public Coordinate GetUpLeft()
        {
            return new Coordinate(Riga - 1, Colonna - 1);
        }
        public Coordinate GetUpRight()
        {
            return new Coordinate(Riga - 1, Colonna + 1);
        }
        public bool IsValid(long maxValue)
        {
            return Riga >= 0 && Colonna >= 0 && Riga < maxValue && Colonna < maxValue;
        }
    }
}
