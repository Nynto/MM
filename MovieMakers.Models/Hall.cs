using System.ComponentModel.DataAnnotations;

namespace MovieMakers.Models
{
    public class Hall
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}