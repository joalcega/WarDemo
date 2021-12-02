using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace AspenCapital.Data.War.Entities
{
    [ExcludeFromCodeCoverage, Table("Suits"), Index("Name", IsUnique = true)]
    public class Suit
    {
        [MaxLength(32)]
        public string Color { get; set; } = null!;

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(32)]
        public string Name { get; set; } = null!;
    }
}
