namespace AspenCapital.Models.War
{
    public class Card
    {
        public string SuitName { get; set; } = null!;

        public string Color { get; set; } = null!;

        public string ValueName { get; set; } = null!;

        public string Symbol { get; set; } = null!;

        public int Weight { get; set; }

        public bool FaceUp { get; set; }
    }
}