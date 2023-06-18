using CineMax.Application.ViewModels;
using CineMax.Core.Repositories;
using MediatR;

namespace CineMax.Application.Queries.GetMyTickets
{
    public class GetMyTicketsQueryHandler : IRequestHandler<GetMyTicketsQuery, List<TicketViewModel>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ITicketRepository _ticketRepository;

        public GetMyTicketsQueryHandler(IClientRepository clientRepository, ITicketRepository ticketRepository)
        {
            _clientRepository = clientRepository;
            _ticketRepository = ticketRepository;
        }

        public async Task<List<TicketViewModel>> Handle(GetMyTicketsQuery request, CancellationToken cancellationToken)
        {
           var client = await _clientRepository.GetByIdAsync(x => x.UserId == request.UserId);
           
            if(client is null)
            {
                return null;
            }

            var ticketViewModel = await _ticketRepository.GetTicketsByClientIdAsync(client.Id);

            return ticketViewModel;
        }
    }
}
