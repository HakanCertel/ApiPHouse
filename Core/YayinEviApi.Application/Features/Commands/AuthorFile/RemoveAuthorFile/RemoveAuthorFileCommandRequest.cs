using MediatR;

namespace YayinEviApi.Application.Features.Commands.AuthorFile.RemoveAuthorFile
{
    public class RemoveAuthorFileCommandRequest : IRequest<RemoveAuthorFileCommandResponse>
    {
        public string Id { get; set; }
        public string? ImageId { get; set; }
    }
}
