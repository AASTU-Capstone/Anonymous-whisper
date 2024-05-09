using ComplaintSystem.Application.Features.ChatBots.Requests.Queries;
using ComplaintSystem.Application.Persistence.Contracts.Common;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.ChatBots.Handlers.Queries;
public class GetChatMessageRequestHandler : IRequestHandler<GetChatMessageRequest, BaseResponseClass>
{
    private readonly IOpenAiServices _openAiServices;
    public GetChatMessageRequestHandler(IOpenAiServices openAiServices)
    {
        _openAiServices = openAiServices;
    }
    public async Task<BaseResponseClass> Handle(GetChatMessageRequest request, CancellationToken cancellationToken)
    {
        var message = await _openAiServices.MessageChat(request.Message);
        BaseResponseClass response = new BaseResponseClass
        {
            Data = message,
            Message = "Chat Fetched Successfully",
            StatusCode = 200,
            Success = true,
        };

        return response;
    }
}
