using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace AspenCapital.Data.War.Entities
{
    [ExcludeFromCodeCoverage, Table("Games")]
    public class Game
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public virtual List<GameMovement> Movements { get; set; } = new List<GameMovement>();

        public DateTime Date { get; set; }

        [ForeignKey("Player1Id")]
        public virtual Player Player1 { get; set; } = null!;

        public Guid Player1Id { get; set; }

        [ForeignKey("Player2Id")]
        public virtual Player Player2 { get; set; } = null!;

        public Guid Player2Id { get; set; }

        public int TotalMovements { get; set; }

        [ForeignKey("WinnerId")]
        public virtual Player Winner { get; set; } = null!;

        public Guid WinnerId { get; set; }
    }
}
