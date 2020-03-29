using System.ComponentModel.DataAnnotations;

namespace MovieMakers.Models
{
    public class AgeGroup
    {
        [Key]
        public int Id { get; set; } [Display(Name="Age group")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
    }
}