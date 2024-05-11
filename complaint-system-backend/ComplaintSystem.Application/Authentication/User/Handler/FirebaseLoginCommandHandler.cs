using AutoMapper;
using FirebaseAdmin.Auth;
using MediatR;
using Newtonsoft.Json.Linq;
using ComplaintSystem.Application.Authentication.common;
using ComplaintSystem.Application.Authentication.User.Request;
using ComplaintSystem.Application.DTOs.UserDto;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Persistence.Contracts.Auth;
using ComplaintSystem.Application.Responses;
using ComplaintSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Authentication.User.Handler;

public class FirebaseLoginCommandHandler : IRequestHandler<FirebaseLoginCommand, AuthenticationResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    public FirebaseLoginCommandHandler(
        IUserRepository userRepository,
        IMapper mapper,
        IJwtTokenGenerator jwtTokenGenerator
        )
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    public async Task<AuthenticationResult> Handle(FirebaseLoginCommand request, CancellationToken cancellationToken)
    {
        BaseResponseClass response;
        try
        {
            FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(request.fbToken);
            bool flag = false;
            var token = "";
            if (decodedToken == null)
            {
                response = new BaseResponseClass
                {
                    Message = "failed login",
                    Success = false
                };
                return new AuthenticationResult(null, token, response.Success, response.Message, flag, response.StatusCode);
            }
            else
            {
                var claims = decodedToken.Claims;
                var email = claims["email"].ToString();
                var user = await _userRepository.GetByEmail(email);
                if (user == null)
                {
                    CreateUserDto userEntity = new CreateUserDto
                    {
                        Email = email,
                        Password = "Pass@1234"
                    };
                    var newUser = _mapper.Map<UserEntity>(userEntity);
                    newUser.Name = "user";
                    await _userRepository.Add(newUser);
                    user = newUser;
                }
                token = _jwtTokenGenerator.GenerateToken(user,true);
                flag = true;
                response = new BaseResponseClass
                {
                    Success = true,
                    Message = "User logged in successfully",
                    StatusCode = 200
                };
                return new AuthenticationResult(user, token, response.Success, response.Message, flag, response.StatusCode);
            }
        }
            
        catch(Exception ex)
        {
            response = new BaseResponseClass
            {
                Message = ex.Message,
                Success = false,
                StatusCode= 400
            };
            return new AuthenticationResult(null, null, response.Success, response.Message, false, response.StatusCode);
        }

    }
}