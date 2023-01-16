namespace CineMax.Core.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }
        public List<Ticket> MyTickets { get; private set; }

        public User(string fullName, string email, string password, string role)
        {
            FullName = fullName;
            Email = email;
            Password = password;
            Role = role;
            CreatedAt = DateTime.Now;
            MyTickets = new List<Ticket>();
        }
    }
}
