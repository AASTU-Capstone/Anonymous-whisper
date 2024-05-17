using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplaintSystem.Application.Responses;
using Microsoft.AspNetCore.Http;


namespace  ComplaintSystem.Application.Persistence.Contracts.Cloudinary
{
    public interface ICloudinaryService
    {
        Task<CloudinaryResponse> UploadImageAsync(IFormFile imageFile);
        Task<CloudinaryResponse> DeleteFile(string publicId);

    }
}
