namespace Football_Manager.Models.Request
{
    public class TransferAllPlayersFromTeam
    {

        public int CurrentTeamId { get; set; }

        public int NewTeamId { get; set; }
    }
}
