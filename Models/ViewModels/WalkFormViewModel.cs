using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models.ViewModels
{
    public class WalkFormViewModel
    {
        public List<Dog> Dogs { get; set; }

        [Required]
        public DateTime WalkDate { get; set; }
    }
}
