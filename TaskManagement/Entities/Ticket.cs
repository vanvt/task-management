namespace TaskManagement.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string States { get; set; }
        public DateTime DeadLine { get; set; }
        public bool IsFavorited { get; set; }
        public List<TicketImage> TicketImages { get; set; }
    }
}
