using AspenCapital.Api.War.Controllers;
using AspenCapital.Data.War;
using AspenCapital.Data.War.Entities;
using AspenCapital.Models.War;
using AspenCapital.Services.War;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace AspenCapital.Test.War
{
    [TestClass]
    public class GameControllerTest
    {
        private WarContext _context { get; set; }

        public GameControllerTest()
        {
            var options = new DbContextOptionsBuilder<WarContext>()
                     .UseInMemoryDatabase(Guid.NewGuid().ToString())
                     .Options;
            _context = new WarContext(options);

            _context.Players.AddRange(
                new Player { Id = new Guid("04C0B9DF-A162-4FC8-FB52-08D9A3BEEB17"), Name = "Player A" },
                new Player { Id = new Guid("04C0B9DF-A162-4FC8-FB52-08D9A3BEEB18"), Name = "Player B" });

            _context.CardValues.AddRange(
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

            _context.Suits.AddRange(
                new Suit { Id = 1, Name = "Clubs", Color = "Black" },
                new Suit { Id = 2, Name = "Diamonds", Color = "Red" },
                new Suit { Id = 3, Name = "Hearts", Color = "Red" },
                new Suit { Id = 4, Name = "Spades", Color = "Black" }
            );

            _context.Games.Add(new Game()
            {
                Id = new Guid("04C0B9DF-A162-4FC8-FB52-08D9A3BEEB16"),
                Date = DateTime.Now,
                Player1Id = new Guid("04C0B9DF-A162-4FC8-FB52-08D9A3BEEB17"),
                Player2Id = new Guid("04C0B9DF-A162-4FC8-FB52-08D9A3BEEB18"),
                WinnerId = new Guid("04C0B9DF-A162-4FC8-FB52-08D9A3BEEB18"),
                TotalMovements = 1
            });

            _context.GameMovements.AddRange(
                new GameMovement() 
                { 
                    Id = 1, 
                    IsWar = false, 
                    GameId = new Guid("04C0B9DF-A162-4FC8-FB52-08D9A3BEEB16"),
                    Number = 1, 
                    Player1DeckCount = 1, 
                    Player2DeckCount = 1, 
                    Player1WonCardsCount = 1, 
                    Player2WonCardsCount = 1,
                    Player1Cards = JsonSerializer.Serialize(
                        new List<Card>()
                        {
                            new Card() { Color = "Red", FaceUp = true, SuitName = "Hearts", Symbol = "Q", ValueName = "Queen", Weight = 11 },
                        }
                    ),
                    Player2Cards = JsonSerializer.Serialize(
                        new List<Card>()
                        {
                            new Card() { Color = "Red", FaceUp = true, SuitName = "Hearts", Symbol = "K", ValueName = "King", Weight = 12 }
                        }
                    )
                });

            _context.SaveChanges();
        }

        [TestMethod]
        public void TestCreate()
        {
            var warProcessor = new WarProcessor(_context);
            var controller = new GameController(warProcessor);
            var response = controller.Create(new Models.War.Requests.CreateGame()
            {
                Player1Name = "Player A",
                Player2Name = "Player B"
            }) as CreatedResult;

            Assert.AreEqual(201, response?.StatusCode);
        }

        [TestMethod]
        public void TestGet()
        {
            var warProcessor = new WarProcessor(_context);
            var controller = new GameController(warProcessor);
            var response = controller.Get(new Guid("04C0B9DF-A162-4FC8-FB52-08D9A3BEEB16")) as OkObjectResult;

            Assert.AreEqual(200, response?.StatusCode);
        }

        [TestMethod]
        public void TestGetMovement()
        {
            var warProcessor = new WarProcessor(_context);
            var controller = new GameController(warProcessor);
            var response = controller.GetMovement(new Guid("04C0B9DF-A162-4FC8-FB52-08D9A3BEEB16"), 1) as OkObjectResult;

            Assert.AreEqual(200, response?.StatusCode);
        }
    }
}