namespace CineMax.Application.ViewModels
{
    public class BuyTicketViewModel
    {
        public List<string> Errors { get; set; } = new List<string>();
        public bool Success { get; set; } = false;  
        public void AddError(string error)
        {
            Errors.Add(error);  
        }
    }
}