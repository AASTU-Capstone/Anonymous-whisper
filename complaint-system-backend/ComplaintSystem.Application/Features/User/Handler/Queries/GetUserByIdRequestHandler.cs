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
    public class GetUserByIdRequestHandler: IRequestHandler<GetUserByIdRequest, BaseResponseClass>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetUserByIdRequestHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<BaseResponseClass> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.Id);
            var result = _mapper.Map<GetUserDto>(user);
            var response = new BaseResponseClass();

            if (result == null)
            {
                response = new BaseResponseClass
                {
                    Message = "User not found",
                    Success = false,
                    StatusCode = 400,
                    Error = new List<string> { "User not found" }
                };
            }

            else
            {
                response = new BaseResponseClass
                {
                    Message = "User retrieved successfully",
                    Success = true,
                    StatusCode = 200,
                    Data = result
                };
            }

            return response;
        }
    }
}