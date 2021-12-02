namespace AspenCapital.Models.War
{
    public class GameDetails
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public string Player1Name { get; set; } = null!;

        public string Player2Name { get; set; } = null!;

        public string Winner { get; set; } = null!;

        public int TotalMovements { get; set; }
    }
}
