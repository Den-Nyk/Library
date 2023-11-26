namespace Library.Models
{
    public class UpdateHeartedStatusModel
    {
        public int BookId { get; set; }
        public string Email { get; set; }
        public bool isHearted { get; set; }
    }
}
