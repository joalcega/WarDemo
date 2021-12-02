using AspenCapital.Data.War;
using AspenCapital.Data.War.Entities;
using AspenCapital.Models.War;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AspenCapital.Services.War
{
    public class WarProcessor : IWarProcessor
    {
        private WarContext _context;
        private Random _random;

        public WarProcessor(WarContext context)
        {
            _context = context;
            _random = new Random();
        }

        public GameDetails CreateGame(string player1Name, string player2Name)
        {
            var date = DateTime.UtcNow;
            var player1 = _context.Players.Single(p => p.Name == player1Name);
            var player2 = _context.Players.Single(p => p.Name == player2Name);

            var suits = _context.Suits.ToList();
            var cardValues = _context.CardValues.ToList();

            var deck = new List<Card>();

            foreach(var suit in suits)
            {
                foreach(var cardValue in cardValues)
                {
                    deck.Add(new Card()
                    {
                        SuitName = suit.Name,
                        Color = suit.Color,
                        ValueName = cardValue.Name,
                        Symbol = cardValue.Symbol,
                        Weight = cardValue.Weight,
                        FaceUp = false
                    });
                }
            }

            deck = Shuffle(deck);

            var half = deck.Count / 2;

            var player1Deck = new Queue<Card>(deck.Take(half));
            var player2Deck = new Queue<Card>(deck.Take(new Range(half, half + half)));

            var total = player1Deck.Count() + player2Deck.Count();

            var player1WonCards = new List<Card>();
            var player2WonCards = new List<Card>();

            bool? player1Wins = null;
            var movements = new List<Movement>();
            var movementCount = 0;

            while (player1WonCards.Count < total && player2WonCards.Count < total && player1Wins == null)
            {
                if (player1Deck.Count == 0)
                {
                    if (player1WonCards.Count > 0)
                    {
                        player1Deck = new Queue<Card>(Shuffle(player1WonCards));
                        player1WonCards.Clear();
                    }
                    else
                    {
                        player1Wins = false;
                        break;
                    }
                }

                if (player2Deck.Count == 0)
                {
                    if (player2WonCards.Count > 0)
                    {
                        player2Deck = new Queue<Card>(Shuffle(player2WonCards));
                        player2WonCards.Clear();
                    }
                    else
                    {
                        player1Wins = true;
                        break;
                    }
                }

                var player1Card = player1Deck.Dequeue();
                player1Card.FaceUp = true;

                var player2Card = player2Deck.Dequeue();
                player2Card.FaceUp = true;

                var movement = new Movement()
                {
                    IsWar = false,
                    Player1Cards = new List<Card>() { player1Card },
                    Player2Cards = new List<Card>() { player2Card }
                };

                if (player1Card.Weight > player2Card.Weight)
                {
                    player1WonCards.Add(player1Card);
                    player1WonCards.Add(player2Card);
                }
                else if (player2Card.Weight > player1Card.Weight)
                {
                    player2WonCards.Add(player1Card);
                    player2WonCards.Add(player2Card);
                }
                else
                {
                    movement.IsWar = true;
                    bool warIsOver = false;

                    while(warIsOver == false)
                    {
                        if (player1Deck.Count < 2)
                        {
                            if (player1WonCards.Count > 0)
                            {
                                var temp = new List<Card>();
                                if (player1Deck.Count > 0)
                                {
                                    temp.Add(player1Deck.Dequeue());
                                }

                                temp.AddRange(Shuffle(player1WonCards));

                                player1Deck = new Queue<Card>(temp);
                                player1WonCards.Clear();
                            }
                            else
                            {
                                player1Wins = false;
                                break;
                            }
                        }

                        if (player2Deck.Count < 2)
                        {
                            if (player2WonCards.Count > 0)
                            {
                                var temp = new List<Card>();
                                if (player2Deck.Count > 0)
                                {
                                    temp.Add(player2Deck.Dequeue());
                                }

                                temp.AddRange(Shuffle(player2WonCards));

                                player2Deck = new Queue<Card>(temp);
                                player2WonCards.Clear();
                            }
                            else
                            {
                                player1Wins = true;
                                break;
                            }
                        }

                        player1Card = player1Deck.Dequeue();
                        player1Card.FaceUp = false;
                        movement.Player1Cards.Add(player1Card);

                        player2Card = player2Deck.Dequeue();
                        player2Card.FaceUp = false;
                        movement.Player2Cards.Add(player2Card);

                        player1Card = player1Deck.Dequeue();
                        player1Card.FaceUp = true;
                        movement.Player1Cards.Add(player1Card);

                        player2Card = player2Deck.Dequeue();
                        player2Card.FaceUp = true;
                        movement.Player2Cards.Add(player2Card);

                        if (player1Card.Weight > player2Card.Weight)
                        {
                            player1WonCards.AddRange(movement.Player1Cards);
                            player1WonCards.AddRange(movement.Player2Cards);
                            warIsOver = true;
                        }
                        else if (player2Card.Weight > player1Card.Weight)
                        {
                            player2WonCards.AddRange(movement.Player1Cards);
                            player2WonCards.AddRange(movement.Player2Cards);
                            warIsOver = true;
                        }
                    }
                }

                movement.Player1Cards = new List<Card>(movement.Player1Cards);
                movement.Player2Cards = new List<Card>(movement.Player2Cards);

                movement.Number = ++movementCount;
                movement.Player1DeckCount = player1Deck.Count;
                movement.Player2DeckCount = player2Deck.Count;
                movement.Player1WonCardsCount = player1WonCards.Count;
                movement.Player2WonCardsCount = player2WonCards.Count;
                movements.Add(movement);
            }

            if (player1Wins == null)
            {
                player1Wins = player1WonCards.Count == total;
            }

            var game = new Game()
            {
                Date = date,
                Player1Id = player1.Id,
                Player2Id = player2.Id,
                WinnerId = player1Wins.Value ? player1.Id : player2.Id,
                Movements = movements.Select(m => new GameMovement()
                {
                    IsWar = m.IsWar,
                    Number = m.Number,
                    Player1Cards = JsonSerializer.Serialize(m.Player1Cards),
                    Player2Cards = JsonSerializer.Serialize(m.Player2Cards),
                    Player1DeckCount= m.Player1DeckCount,
                    Player1WonCardsCount = m.Player1WonCardsCount,
                    Player2DeckCount = m.Player2DeckCount,
                    Player2WonCardsCount = m.Player2WonCardsCount
                }).ToList(),
                TotalMovements = movementCount
            };

            _context.Games.Add(game);
            _context.SaveChanges();

            return new GameDetails()
            {
                Id = game.Id,
                Date = game.Date,
                Player1Name = player1.Name,
                Player2Name = player2.Name,
                Winner = player1Wins.Value ? player1.Name : player2.Name,
                TotalMovements = movementCount
            };
        }

        public GameDetails GetGameDetails(Guid gameId)
        {
            var game = _context.Games
                .Include(g => g.Player1)
                .Include(g => g.Player2)
                .Include(g => g.Winner)
                .First(g => g.Id == gameId);

            return new GameDetails()
            {
                Id = game.Id,
                Date = game.Date,
                Player1Name = game.Player1.Name,
                Player2Name = game.Player2.Name,
                Winner = game.Winner.Name,
                TotalMovements = game.TotalMovements
            };
        }

        public Movement GetMovement(Guid gameId, int number)
        {
            var movement = _context.GameMovements.Single(m => m.GameId == gameId && m.Number == number);

            return new Movement()
            {
                IsWar = movement.IsWar,
                Number = movement.Number,
                Player1Cards = JsonSerializer.Deserialize<List<Card>>(movement.Player1Cards)!,
                Player2Cards = JsonSerializer.Deserialize<List<Card>>(movement.Player2Cards)!,
                Player1DeckCount = movement.Player1DeckCount,
                Player2DeckCount = movement.Player2DeckCount,
                Player1WonCardsCount = movement.Player1WonCardsCount,
                Player2WonCardsCount = movement.Player2WonCardsCount
            };
        }

        public List<GameDetails> GetWinsByPlayer(string playerName)
        {
            var player = _context.Players.Single(p => p.Name == playerName);

            var games = _context.Games
                .Include(g => g.Player1)
                .Include(g => g.Player2)
                .Include(g => g.Winner)
                .Where(g => g.WinnerId == player.Id)
                .ToList();

            return games.Select(game => new GameDetails()
            {
                Id = game.Id,
                Date = game.Date,
                Player1Name = game.Player1.Name,
                Player2Name = game.Player2.Name,
                Winner = game.Winner.Name,
                TotalMovements = game.TotalMovements
            })
            .ToList();
        }

        public List<Card> Shuffle(List<Card> deck)
        {
            var lastIndex = deck.Count - 1;

            for (int i = 0; i <= lastIndex; i++)
            {
                int randomIndex = _random.Next(0, lastIndex);
                var temp = deck[randomIndex];
                deck[randomIndex] = deck[i];
                deck[i] = temp;
            }

            return deck;
        }
    }
}