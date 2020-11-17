namespace VivaLaDama.Models
{
    public class Move
    {
        public Pawn Target { get; set; }//The target is the pawn that has been moved
        public Coordinate From { get; set; }
        public Coordinate To { get; set; }
        public Move(Pawn target, Coordinate to)
        {
            this.Target = target;
            this.From = null;
            this.To = to;
        }
    }
}
