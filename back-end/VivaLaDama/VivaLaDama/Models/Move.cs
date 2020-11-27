namespace VivaLaDama.Models
{
    public class Move
    {
        public int IdMossa { get; set; }
        public Pawn Target { get; set; }//The target is the pawn that has been moved
        public Coordinate From { get; set; }
        public Coordinate To { get; set; }
        public Move()
        {
            this.IdMossa = -1;
            this.Target = null;
            this.From = null;
            this.To = null;
        }
        public Move(Pawn target, Coordinate to)
        {
            this.IdMossa = -1;
            this.Target = target;
            this.From = null;
            this.To = to;
        }
    }
}
