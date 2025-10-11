using MediatR;
using Microsoft.EntityFrameworkCore;
using YayinEviApi.Application.Repositories.IAuthorR;

namespace YayinEviApi.Application.Features.Commands.AuthorFile.RemoveAuthorFile
{
    public class RemoveAuthorFileCommandHandler : IRequestHandler<RemoveAuthorFileCommandRequest, RemoveAuthorFileCommandResponse>
    {

        readonly IAuthorReadRepository _authorReadRepository;
        readonly IAuthorWriteRepository _authorWriteRepository;

        public RemoveAuthorFileCommandHandler(IAuthorReadRepository authorReadRepository, IAuthorWriteRepository authorWriteRepository)
        {
            _authorReadRepository = authorReadRepository;
            _authorWriteRepository = authorWriteRepository;
        }

        public async Task<RemoveAuthorFileCommandResponse> Handle(RemoveAuthorFileCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Author? author = await _authorReadRepository.Table.Include(p => p.AuthorFiles)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));

            Domain.Entities.AuthorFile? authorFile = author?.AuthorFiles.FirstOrDefault(p => p.Id == Guid.Parse(request.ImageId));

            if (authorFile != null)
                author?.AuthorFiles.Remove(authorFile);

            await _authorWriteRepository.SaveAsync();
            return new();
        }
    }
}
