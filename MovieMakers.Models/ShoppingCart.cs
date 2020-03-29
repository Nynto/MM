using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieMakers.Models
{
    
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            CountAdult = 0;
            CountEldKids = 0;
        }

        [Key]
        public int Id  { get; set; }

        public string ApplicationUserId  { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
        // public int MovieId  { get; set; }
        // [ForeignKey("MovieId")]
        //
        // // public Movie Movie { get; set; }
        public int EventId  { get; set; }
        [ForeignKey("EventId")]
        public Event Event { get; set; }
        
        
        // public Hall Hall { get; set; }
        public int CountAdult { get; set; }
        public int CountEldKids { get; set; }

        [NotMapped] 
        public double Price { get; set; }
        
    }
}