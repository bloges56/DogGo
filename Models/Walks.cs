using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models
{
    public class Walks
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public int WalkerId { get; set; }

        public Walker Walker { get; set; }

        [DisplayName("Dog")]
        [Required]
        public int DogId { get; set; }

        public Dog Dog { get; set; }

        [Required]
        public int WalkStatusId { get; set; }

        public string WalkStatus { get; set; }


    }
}
