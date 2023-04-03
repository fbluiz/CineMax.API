using CineMax.Core.Repositories;
using MediatR;

namespace CineMax.Application.Commands.DeleteMovieCommand
{
    public class DeleteSectionCommandHandler : IRequestHandler<DeleteSectionCommand,Unit>
    {
        private readonly ISectionRepository _sectionRepository;

        public DeleteSectionCommandHandler(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public async Task<Unit> Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
        {
            var section = await _sectionRepository.GetByIdAsync(m => m.Id == request.Id);

            section.delete();

            await _sectionRepository.DeleteAsync(section);

            return Unit.Value; 
        }
    }
}
