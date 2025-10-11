using MediatR;
using Microsoft.EntityFrameworkCore;
using YayinEviApi.Application.Repositories.AgencyR;
using YayinEviApi.Domain.Entities.AgencyE;

namespace YayinEviApi.Application.Features.Commands.AgencyFile.RemoveAgencyFile
{
    public class RemoveAgencyFileCommandHandler : IRequestHandler<RemoveAgencyFileCommandRequest, RemoveAgencyFileCommandResponse>
    {

        readonly IAgencyReadRepository _agencyReadRepository;
        readonly IAgencyWriteRepository _agencywriteRepository;

        public RemoveAgencyFileCommandHandler(  IAgencyReadRepository agencyReadRepository, IAgencyWriteRepository agencywriteRepository)
        {
            _agencyReadRepository = agencyReadRepository;
            _agencywriteRepository = agencywriteRepository;
        }

        public async Task<RemoveAgencyFileCommandResponse> Handle(RemoveAgencyFileCommandRequest request, CancellationToken cancellationToken)
        {
            Agency? agency = await _agencyReadRepository.Table.Include(p => p.AgencyFiles)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));

            Domain.Entities.AgencyFile? agencyFile = agency?.AgencyFiles.FirstOrDefault(p => p.Id == Guid.Parse(request.ImageId));

            if (agencyFile != null)
                agency?.AgencyFiles.Remove(agencyFile);

            await _agencywriteRepository.SaveAsync();
            return new();
        }
    }
}
