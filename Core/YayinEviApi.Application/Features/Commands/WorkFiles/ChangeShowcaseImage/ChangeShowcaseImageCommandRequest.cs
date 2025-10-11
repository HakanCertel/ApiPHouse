using MediatR;

namespace YayinEviApi.Application.Features.Commands.WorkFiles.ChangeShowcaseImage
{
    public class ChangeShowcaseImageCommandRequest : IRequest<ChangeShowcaseImageCommandResponse>
    {
        public string ImageId { get; set; }
        public string Id { get; set; }
        public string? UserId { get; set; }
        public string? DepartmentId { get; set; }
    }
}
