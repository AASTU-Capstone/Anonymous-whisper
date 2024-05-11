using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Listings.Requests.Commands;
public class DeleteListingCommand : IRequest<BaseResponseClass>
{
    public Guid Id { get; set; }
}
