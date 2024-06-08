using ComplaintSystem.Application.DTOs.ResourceDto;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Resources.Request.Commands;
public class CreateResourceCommand : IRequest<BaseResponseClass>
{
    public CreateResourceDto createResourceDto { get; set; }
}
