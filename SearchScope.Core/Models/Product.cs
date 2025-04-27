using System.ComponentModel.DataAnnotations;

namespace SearchScopeAPI.SearchScope.Core.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [StringLength(300)]
        public string? Description { get; set; }

        public string? Category { get; set; }

        public decimal? UnitPrice { get; set; }

        public short? UnitsInStock { get; set; }

        [Range(1, 10)]
        public decimal Popularity { get; set; }

        public bool Discontinued { get; set; }

        public DateTime ProductionData { get; set; }
    }
}
