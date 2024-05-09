using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.ChatBots.Requests.Queries;
public class GetChatMessageRequest : IRequest<BaseResponseClass>
{
    public string Message { get; set; }
}
