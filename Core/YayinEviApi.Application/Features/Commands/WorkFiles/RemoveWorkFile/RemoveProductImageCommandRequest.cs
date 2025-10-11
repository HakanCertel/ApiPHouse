using MediatR;

namespace YayinEviApi.Application.Features.Commands.WorkFiles.RemoveWorkFile
{
    public class RemoveWorkFileCommandRequest : IRequest<RemoveWorkFileCommandResponse>
    {
        public string Id { get; set; }
        public string? ImageId { get; set; }
    }
}
