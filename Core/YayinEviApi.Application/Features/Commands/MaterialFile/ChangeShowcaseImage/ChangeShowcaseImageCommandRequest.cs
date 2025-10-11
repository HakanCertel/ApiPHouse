using MediatR;

namespace YayinEviApi.Application.Features.Commands.MaterailFile.ChangeShowcaseImage
{
    public class ChangeShowcaseImageCommandRequest : IRequest<ChangeShowcaseImageCommandResponse>
    {
        public string Id { get; set; }
        public string ImageId { get; set; }
    }
}
