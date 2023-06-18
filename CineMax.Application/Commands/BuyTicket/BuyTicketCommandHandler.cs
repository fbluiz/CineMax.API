using CineMax.Application.ViewModels;
using CineMax.Core.DTOs.PaymentDTOs.Request;
using CineMax.Core.Entities;
using CineMax.Core.Enums;
using CineMax.Core.Repositories;
using CineMax.Core.Services.Auth;
using CineMax.Core.Services.Payment;
using MediatR;

namespace CineMax.Application.Commands.BuyTicket
{
    public class BuyTicketCommandHandler : IRequestHandler<BuyTicketCommand, BuyTicketViewModel>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IPaymentService _paymentService;

        public BuyTicketCommandHandler(IRoomRepository roomRepository, ISectionRepository sectionRepository, ITicketRepository ticketRepository, IClientRepository clientRepository, IPaymentService paymentService)
        {
            _roomRepository = roomRepository;
            _sectionRepository = sectionRepository;
            _ticketRepository = ticketRepository;
            _clientRepository = clientRepository;
            _paymentService = paymentService;
        }

        public async Task<BuyTicketViewModel> Handle(BuyTicketCommand request, CancellationToken cancellationToken)
        {
            var section = await _sectionRepository.GetByIdAsync(s => s.Id == request.SectionId);
            var response = new BuyTicketViewModel { };

            if (section.Status != SectionStatusEnum.Created)
            {
                response.AddError("Section " + section.Name + " no disponible");

                return response;    
            }

            if (section.TicketsDisponible <= 0)
            {
                response.Errors.Add("Tickets exhausted");
                return response;
            }

            var seatSectionStatus = (await _roomRepository.GetSeatsStatusBySection(request.SectionId, request.SeatId)).First();

            if (!seatSectionStatus.IsDisponible)
            {
                response.AddError("The seat " + seatSectionStatus.Position + " no disponible");
                return response;
            }
            PaymentRequest paymentRequest = new PaymentRequest
            {
                Cpf = request.Cpf,
                DateExpration = request.DateExpration,
                CVV = request.CVV,
                NameClientOfCard = request.NameClientOfCard,
                NumberCard = request.NumberCard,
            };

            bool paymentStatus = (_paymentService.Process(paymentRequest)).Success;

            if (!paymentStatus)
            {
                response.AddError("Payment Recused");
                return response;
            }
            
            int clientId = (await _clientRepository.GetByIdAsync(c => c.UserId == request.UserId)).Id;

            Ticket ticket = new Ticket(section.Id, clientId, seatSectionStatus.Id);
            await _ticketRepository.AddAsync(ticket);

            section.SubtractTicketsDisponible();
            await _sectionRepository.UpdateAsync(section);

            var seatStatus = await _roomRepository.GetSectionSeatAsync(section.Id,seatSectionStatus.Id);

            seatStatus.ChangeAvailability(false);
            await _roomRepository.UpdateSectionSeatAsync(seatStatus);

            if (response.Errors.Count == 0)
            {
                response.Success = true;
            }
            return response;
        }
    }
}
