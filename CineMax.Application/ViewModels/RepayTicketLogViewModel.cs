namespace CineMax.Application.ViewModels
{
    public class RepayTicketLogViewModel
    {
        public List<string> Errors { get; set; } = new List<string>();
        public bool Success { get; set; } = false;
        public bool TicketBelongstoCustomer { get; set; } = true;
        public void AddError(string error)
        {
            Errors.Add(error);
        }
    }
}
