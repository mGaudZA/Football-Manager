namespace Football_Manager.Models.Request
{
    public class LinkTeamToStadiumRequest
    {
        public int StadiumId { get; set; }
        public int TeamId { get; set; }
    }
}
