using Football_Manager.Enums;
using System.ComponentModel.DataAnnotations;

namespace Football_Manager.Models.Tables
{
    public class Player
    {
        public int PlayerId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }  
        
        public Positions Position { get; set; }

        public int NumberOfYellowCards { get; set; }

        public int NumberOfRedCards { get; set; }

        public int NumberOfGoalsScored { get; set; }

        public int TeamId { get; set; }

        public string PortraitKey { get; set; }
    }
}
