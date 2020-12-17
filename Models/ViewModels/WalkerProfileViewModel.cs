using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models.ViewModels
{
    public class WalkerProfileViewModel
    {
        public Walker Walker { get; set; }
        public List<Walks> Walks { get; set; }

        public string TotalDurationString
        {
            get
            {
                int totalSeconds = Walks.Sum(walk => walk.Duration);
                TimeSpan TotalDuration = TimeSpan.FromSeconds(totalSeconds);
                return $"{TotalDuration.Hours}hr {TotalDuration.Minutes}min";
            }
        }
    }
}
