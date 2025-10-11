using MediatR;

namespace YayinEviApi.Application.Features.Commands.AgencyFile.RemoveAgencyFile
{
    public class RemoveAgencyFileCommandRequest : IRequest<RemoveAgencyFileCommandResponse>
    {
        public string Id { get; set; }
        public string? ImageId { get; set; }
    }
}
