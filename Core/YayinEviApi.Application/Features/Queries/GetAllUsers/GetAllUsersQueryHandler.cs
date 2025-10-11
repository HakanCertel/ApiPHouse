using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.DTOs.User;

namespace YayinEviApi.Application.Features.Queries.AppUser.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQueryRequest, GetAllUsersQueryResponse>
    {
        readonly IUserService _userService;

        public GetAllUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetAllUsersQueryResponse> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
        {
            List<CreateUser> userList = _userService.GetAllUsersAsync().Result;
            
            if (request.DepartmentName != null && userList.Count>0) {
                userList=userList.Where(x=>x.DepartmentName==request.DepartmentName).ToList();
                return new()
                {
                    Users= userList,
                    TotalUsersCount = userList.Count
                };
            }
            else
                return new()
                {
                    Users = userList,
                    TotalUsersCount = userList.Count
                };
        }
    }
}
