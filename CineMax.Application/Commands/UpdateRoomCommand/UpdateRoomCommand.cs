using MediatR;

namespace CineMax.Application.Commands.UpdateRoomCommand
{
    public class UpdateRoomCommand: IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsRoomOcuped { get; set; }
        //public Section Section { get; set; }
        //public Section Movie { get; set; }
    }
}
