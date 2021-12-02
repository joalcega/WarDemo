using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspenCapital.Data.War.Entities
{
    [ExcludeFromCodeCoverage, Table("GameMovements")]
    public class GameMovement
    {
        [ForeignKey("GameId")]
        public virtual Game Game { get; set; } = null!;

        public Guid GameId { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public bool IsWar { get; set; }

        public int Number { get; set; }

        public string Player1Cards { get; set; } = null!;

        public string Player2Cards { get; set; } = null!;

        public int Player1DeckCount { get; set; }

        public int Player2DeckCount { get; set; }

        public int Player1WonCardsCount { get; set; }

        public int Player2WonCardsCount { get; set; }
    }
}
