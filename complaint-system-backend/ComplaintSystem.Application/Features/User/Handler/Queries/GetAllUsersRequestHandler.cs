using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using   ComplaintSystem.Application.DTOs.UserDto;
using   ComplaintSystem.Application.Features.User.Request.Queries;
using   ComplaintSystem.Application.Persistence.Contracts;
using   ComplaintSystem.Application.Responses;

namespace   ComplaintSystem.Application.Features.User.Handler.Queries
{
    public class GetAllUsersRequestHandler: IRequestHandler<GetAllUsersRequest, BaseResponseClass>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetAllUsersRequestHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<BaseResponseClass> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();
            var result = _mapper.Map<List<GetUserDto>>(users);
            var response = new BaseResponseClass();

            if (result == null)
            {
                response = new BaseResponseClass
                {
                    Message = "No users found",
                    Success = false,
                    StatusCode = 400,
                    Error = new List<string> { "No users found" }
                };
            }

            else
            {
                response = new BaseResponseClass
                {
                    Message = "Users retrieved successfully",
                    Success = true,
                    StatusCode = 200,
                    Data = result
                };
            }

            return response;
        }
    }
}