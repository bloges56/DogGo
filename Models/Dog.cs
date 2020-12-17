using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models
{
    public class Dog
    {
        public int Id { get; set; }

        [Required]
        [StringLength(55, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        public int OwnerId { get; set; }

        public Owner Owner { get; set; }

        [Required]
        [StringLength(55, MinimumLength = 5)]
        public string Breed { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        [StringLength(255)]
        public string ImageUrl { get; set; }
       

    }
}
