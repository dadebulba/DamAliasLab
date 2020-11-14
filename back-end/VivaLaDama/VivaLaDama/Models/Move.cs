namespace VivaLaDama.Models
{
    public class Move
    {
        public Pawn Target { get; set; }//The target is the pawn that has been moved
        public Coordinates From { get; set; }
        public Coordinates To { get; set; }
        public Move(Pawn target, Coordinates from, Coordinates to)
        {
            this.Target = target;
            this.From = from;
            this.To = to;
        }
    }
}
