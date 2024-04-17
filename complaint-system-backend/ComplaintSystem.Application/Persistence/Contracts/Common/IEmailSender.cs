using  ComplaintSystem.Application.Responses;
using  ComplaintSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  ComplaintSystem.Application.Persistence.Contracts.Common;

public interface IEmailSender
{
    public Task<BaseResponseClass> SendEmail(Email email);
}
