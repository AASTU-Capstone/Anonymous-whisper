using Microsoft.AspNetCore.Http;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using System.Security.Principal;
using  ComplaintSystem.Application.Persistence.Contracts.Cloudinary;
using ComplaintSystem.Application.Responses;

namespace  ComplaintSystem.Infrastructure.Services;

public class CloudinaryService : ICloudinaryService
{
    private CloudinarySetting _cloudinarySettings { get; }

    public CloudinaryService(IOptions<CloudinarySetting> cloudinarySettings)
    {
        _cloudinarySettings = cloudinarySettings.Value;
    }

    public async Task<CloudinaryResponse> UploadImageAsync(IFormFile imageFile)
    {
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png",".pdf",".docx",".doc",".xlsx",".mp4",".wmv",".mkv",".avi", ".mp3",".ogg" };
        var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
        CloudinaryResponse response;
        if (!allowedExtensions.Contains(extension))
        {
            response = new CloudinaryResponse
            {
                Success = false,
                Message = "Unsupported file format"
            };
        }
        else
        {
            var client = new Cloudinary(new Account(
            _cloudinarySettings.CloudName = Environment.GetEnvironmentVariable("Cloud_Name"),
            _cloudinarySettings.ApiKey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY"),
            _cloudinarySettings.ApiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET")
        ));

            var uploadParams = new AutoUploadParams()
            {
                File = new FileDescription(imageFile.FileName, imageFile.OpenReadStream()),
                //Transformation = new Transformation().Height(500).Width(500).Crop("fill")
            };
            var uploadResult = await client.UploadAsync(uploadParams);
            var publicId = await Task.FromResult(uploadResult.PublicId);
            var link = await Task.FromResult(uploadResult.SecureUrl.AbsoluteUri);
            response = new CloudinaryResponse
            {
                Link = link,
                PublicId = publicId,
                Success = true,
                Message = "File Uploaded Sucessfully"
            };
        }
        
        return response;
    }

    public async Task<CloudinaryResponse> DeleteFile(string publicId)
    {
        var client = new Cloudinary(new Account(
            _cloudinarySettings.CloudName = Environment.GetEnvironmentVariable("Cloud_Name"),
            _cloudinarySettings.ApiKey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY"),
            _cloudinarySettings.ApiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET")
        ));

        DeletionParams deletionParams = new DeletionParams(publicId)
        {
        };
        var deleteResult = await client.DestroyAsync(deletionParams);
        CloudinaryResponse response;
        if(deleteResult.Result.ToLower() == "ok")
        {
            response = new CloudinaryResponse
            {
                Success = true,
            };
        }
        else
        {
            response = new CloudinaryResponse
            {
                Success = false,
            };
        }

        return response;
    }
}