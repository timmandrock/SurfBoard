using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurfAndTurf.Models
{
    public class SurfBoard
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? Name { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string? Type { get; set; }

        public double Length { get; set; }
        public double Width{ get; set; }
        public double Thickness { get; set; }
        public double Volume { get; set; }

        [Range(1, 100000)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public string Equipment { get; set; }

        [Timestamp]
        public byte[] Rowversion { get; set; }
   
        [Display(Name = "Image")]
        public string? ImageUrl { get; set; }

    }
}
