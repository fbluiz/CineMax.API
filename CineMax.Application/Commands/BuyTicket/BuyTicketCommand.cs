using CineMax.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMax.Application.Commands.BuyTicket
{
    public  class BuyTicketCommand : IRequest<BuyTicketViewModel>
    {
        public int SectionId { get; set; }
        public int SeatId { get; set; }
        public Guid UserId { get; set; }
        public int Cpf { get; set; }
        public int NumberCard { get; set; }
        public int CVV { get; set; }
        public DateTime DateExpration { get ; set; }
        public string NameClientOfCard { get; set; }
    }
}
