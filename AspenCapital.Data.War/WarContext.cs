using AspenCapital.Data.War.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspenCapital.Data.War
{
    public class WarContext : DbContext
    {
        public WarContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<CardValue> CardValues { get; set; } = null!;

        public DbSet<Game> Games { get; set; } = null!;

        public DbSet<GameMovement> GameMovements { get; set; } = null!;

        public DbSet<Player> Players { get; set; } = null!;

        public DbSet<Suit> Suits { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SeedData(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CardValue>().HasData(
                new CardValue { Id = 1, Name = "Two", Symbol = "2", Weight = 1 },
                new CardValue { Id = 2, Name = "Three", Symbol = "3", Weight = 2 },
                new CardValue { Id = 3, Name = "Four", Symbol = "4", Weight = 3 },
                new CardValue { Id = 4, Name = "Five", Symbol = "5", Weight = 4 },
                new CardValue { Id = 5, Name = "Six", Symbol = "6", Weight = 5 },
                new CardValue { Id = 6, Name = "Seven", Symbol = "7", Weight = 6 },
                new CardValue { Id = 7, Name = "Eight", Symbol = "8", Weight = 7 },
                new CardValue { Id = 8, Name = "Nine", Symbol = "9", Weight = 8 },
                new CardValue { Id = 9, Name = "Ten", Symbol = "10", Weight = 9 },
                new CardValue { Id = 10, Name = "Jack", Symbol = "J", Weight = 10 },
                new CardValue { Id = 11, Name = "Queen", Symbol = "Q", Weight = 11 },
                new CardValue { Id = 12, Name = "King", Symbol = "K", Weight = 12 },
                new CardValue { Id = 13, Name = "Ace", Symbol = "A", Weight = 13 }
            );

            modelBuilder.Entity<Player>().HasData(
                new Player { Id = new Guid("04C0B9DF-A162-4FC8-FB52-08D9A3BEEB17"), Name = "Player A" },
                new Player { Id = new Guid("04C0B9DF-A162-4FC8-FB52-08D9A3BEEB18"), Name = "Player B" }
            );

            modelBuilder.Entity<Suit>().HasData(
                new Suit { Id = 1, Name = "Clubs", Color = "Black" },
                new Suit { Id = 2, Name = "Diamonds", Color = "Red" },
                new Suit { Id = 3, Name = "Hearts", Color = "Red" },
                new Suit { Id = 4, Name = "Spades", Color = "Black" }
            );
        }
    }
}