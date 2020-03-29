using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieMakers.Models
{
    public class LostAndFound 
    {        
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public string Location { get; set; }
        
        [Required]
        public string Description { get; set; }

        public bool Solved { get; set; }
    }    
}