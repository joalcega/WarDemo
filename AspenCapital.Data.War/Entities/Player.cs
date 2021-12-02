using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace AspenCapital.Data.War.Entities
{
    [ExcludeFromCodeCoverage, Table("Players"), Index("Name", IsUnique = true)]
    public class Player
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [MaxLength(32)]
        public string Name { get; set; } = null!;
    }
}
