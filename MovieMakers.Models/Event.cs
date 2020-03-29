using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieMakers.Models
{
    public class Event 
    {
        
        [Key]
        public int Id { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public int HallId { get; set; }
        [ForeignKey("HallId")] 
        public Hall Hall { get; set; }
        
        [Required]
        public string StartTime { get; set; }
        
        [Required]
        public int MovieId { get; set; }
        [ForeignKey("MovieId")] 
        public Movie Movie { get; set; }
        

    }
    
}