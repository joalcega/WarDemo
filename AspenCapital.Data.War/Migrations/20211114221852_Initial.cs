using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspenCapital.Data.War.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Player1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Player2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalMovements = table.Column<int>(type: "int", nullable: false),
                    WinnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Players_Player1Id",
                        column: x => x.Player1Id,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Games_Players_Player2Id",
                        column: x => x.Player2Id,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Games_Players_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "GameMovements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsWar = table.Column<bool>(type: "bit", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Player1Cards = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Player2Cards = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Player1DeckCount = table.Column<int>(type: "int", nullable: false),
                    Player2DeckCount = table.Column<int>(type: "int", nullable: false),
                    Player1WonCardsCount = table.Column<int>(type: "int", nullable: false),
                    Player2WonCardsCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameMovements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameMovements_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CardValues",
                columns: new[] { "Id", "Name", "Symbol", "Weight" },
                values: new object[,]
                {
                    { 1, "Two", "2", 1 },
                    { 2, "Three", "3", 2 },
                    { 3, "Four", "4", 3 },
                    { 4, "Five", "5", 4 },
                    { 5, "Six", "6", 5 },
                    { 6, "Seven", "7", 6 },
                    { 7, "Eight", "8", 7 },
                    { 8, "Nine", "9", 8 },
                    { 9, "Ten", "10", 9 },
                    { 10, "Jack", "J", 10 },
                    { 11, "Queen", "Q", 11 },
                    { 12, "King", "K", 12 },
                    { 13, "Ace", "A", 13 }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("04c0b9df-a162-4fc8-fb52-08d9a3beeb17"), "Player A" },
                    { new Guid("04c0b9df-a162-4fc8-fb52-08d9a3beeb18"), "Player B" }
                });

            migrationBuilder.InsertData(
                table: "Suits",
                columns: new[] { "Id", "Color", "Name" },
                values: new object[,]
                {
                    { 1, "Black", "Clubs" },
                    { 2, "Red", "Diamonds" },
                    { 3, "Red", "Hearts" },
                    { 4, "Black", "Spades" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardValues_Name",
                table: "CardValues",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameMovements_GameId",
                table: "GameMovements",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Player1Id",
                table: "Games",
                column: "Player1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Player2Id",
                table: "Games",
                column: "Player2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Games_WinnerId",
                table: "Games",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_Name",
                table: "Players",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suits_Name",
                table: "Suits",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardValues");

            migrationBuilder.DropTable(
                name: "GameMovements");

            migrationBuilder.DropTable(
                name: "Suits");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
