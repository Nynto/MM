using System.ComponentModel.DataAnnotations;

namespace MovieMakers.Models
{
    public class StartTime
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}