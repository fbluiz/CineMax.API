namespace CineMax.Core.DTOs.PaymentDTOs.Request
{
    public class PaymentRequest
    {
        public int Cpf { get; set; }
        public int NumberCard { get; set; }
        public int CVV { get; set; }
        public DateTime DateExpration { get; set; }
        public string NameClientOfCard { get; set; }
    }
}
