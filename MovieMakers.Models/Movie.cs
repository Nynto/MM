using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieMakers.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string IMDB { get; set; }
        [Required]
        public string Director { get; set; }
        
        [Required]
        [Range(1, 8.50)]
        [Display(Name="Price")]
        public double ListPrice { get; set; }
        
        [Required]
        [Range(1, 7)]
        [Display(Name="Reduction Price")]
        public double Price { get; set; }
        [Required]
        [Range(1, 10000)]
        public double Price50 { get; set; }

        [Required]
        [Display(Name="Duration (min)")]
        public string Duration { get; set; }
        public string ImageUrl { get; set; }
        
        [Required]
        public int GenreId { get; set; }
        [ForeignKey("GenreId")] public Genre Genre { get; set; }
        [Required]
        public int AgeGroupId { get; set; }
        [ForeignKey("AgeGroupId")] public AgeGroup AgeGroup { get; set; }
        
    }
}