using System.ComponentModel.DataAnnotations;

namespace Football_Manager.Models.Tables
{
    public class Team
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0.0, 100, ErrorMessage = "The field {0} must be greater than {1} and less than {2}.")]
        public double PassPercentage { get; set; }

        [Range(0.0, 100, ErrorMessage = "The field {0} must be greater than {1} and less than {2}.")]
        public double PossessionPercentage { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public List<Player> Players { get; set; }
    }
}
