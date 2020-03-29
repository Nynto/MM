    using System.ComponentModel.DataAnnotations;

namespace MovieMakers.Models
{
    public class Genre
    { 
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
    }
}