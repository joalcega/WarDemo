namespace AspenCapital.Models.War
{
    public class Movement
    {
        public bool IsWar { get; set; }

        public int Number { get; set; }

        public List<Card> Player1Cards { get; set; } = null!;

        public List<Card> Player2Cards { get; set; } = null!;

        public int Player1DeckCount { get; set; }

        public int Player2DeckCount { get; set; }

        public int Player1WonCardsCount { get; set; }

        public int Player2WonCardsCount { get; set; }
    }
}
