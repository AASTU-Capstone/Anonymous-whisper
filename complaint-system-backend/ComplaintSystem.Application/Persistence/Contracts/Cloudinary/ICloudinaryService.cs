using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace  ComplaintSystem.Application.Persistence.Contracts.Cloudinary
{
    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(IFormFile imageFile);

    }
}
