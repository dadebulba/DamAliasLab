namespace VivaLaDama.Models
{
    public class Mossa
    {
        public Pedina Target { get; set; }
        public Coordinate From { get; set; }
        public Coordinate To { get; set; }
        public Mossa(Pedina target, Coordinate from, Coordinate to)
        {
            this.Target = target;
            this.From = from;
            this.To = to;
        }
    }
}
