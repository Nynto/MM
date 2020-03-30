using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieMakers.Models
{
    public class Row
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int HallId { get; set; }
        [ForeignKey("HallId")]
        public Hall Hall { get; set; }

        [Required]
        public int Number { get; set; }

        [Display(Name = "Number of Seats")]
        [Required]
        [Range(1, 20)]
        public int NumberOfSeats { get; set; }
    }
}