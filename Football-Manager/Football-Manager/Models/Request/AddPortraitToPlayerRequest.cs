namespace Football_Manager.Models.Request
{
    public class AddPortraitToPlayerRequest
    {
        public int PlayerId { get; set; }
        public string PortraitBase64String { get; set; }
    }
}
