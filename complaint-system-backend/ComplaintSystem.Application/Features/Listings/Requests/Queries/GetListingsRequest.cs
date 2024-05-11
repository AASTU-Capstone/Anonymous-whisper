using ComplaintSystem.Application.DTOs.ListingsDto;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Listings.Requests.Queries;
public class GetListingsRequest : IRequest<BaseResponseClass>
{
}
