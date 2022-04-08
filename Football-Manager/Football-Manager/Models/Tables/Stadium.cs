using System.ComponentModel.DataAnnotations;

namespace Football_Manager.Models.Tables
{
    public class Stadium
    {
        public int StadiumId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]
        public string Sport { get; set; }

    }
}
