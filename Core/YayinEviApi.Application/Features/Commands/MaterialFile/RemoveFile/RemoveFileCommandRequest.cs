using MediatR;

namespace YayinEviApi.Application.Features.Commands.MaterailFile.RemoveFile
{
    public class RemoveFileCommandRequest : IRequest<RemoveFileCommandResponse>
    {
        public string Id { get; set; }
        public string? ImageId { get; set; }
    }
}
