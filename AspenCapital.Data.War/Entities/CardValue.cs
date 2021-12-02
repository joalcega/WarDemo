using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace AspenCapital.Data.War.Entities
{
    [ExcludeFromCodeCoverage, Table("CardValues"), Index("Name", IsUnique = true)]
    public class CardValue
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(32)]
        public string Name { get; set; } = null!;

        [MaxLength(2)]
        public string Symbol { get; set; } = null!;

        public int Weight { get; set; }
    }
}
