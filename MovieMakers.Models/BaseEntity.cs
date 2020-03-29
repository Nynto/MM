using System;
using System.ComponentModel.DataAnnotations;

namespace MovieMakers.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
