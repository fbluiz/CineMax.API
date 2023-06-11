namespace CineMax.Core.Entities
{
    public class Client : BaseEntity
    {
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Role { get; private set; }
        public Guid UserId { get; private set; }
        public List<Ticket> MyTickets { get; private set; }

        public Client(string fullName, string email, string role, Guid userId)
        {
            FullName = fullName;
            Email = email;
            CreatedAt = DateTime.Now;
            Role = role;
            UserId = userId;
            MyTickets = new List<Ticket>();
        }

        public void addTicket (Ticket ticket)
        {
            MyTickets.Add(ticket); 
        }      
    }
}
