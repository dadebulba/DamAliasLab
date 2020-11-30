namespace VivaLaDama.Models
{
    public class Move
    {
        public int IdMossa { get; set; }
        public Pawn Target { get; set; }//The target is the pawn that has been moved
        public Coordinate From { get; set; }
        public Coordinate To { get; set; }
    }
}
