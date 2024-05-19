using CloudinaryDotNet;
using ComplaintSystem.Application.DTOs.RESTAPIDto;
using ComplaintSystem.Application.Persistence.Contracts.APIs;
using ComplaintSystem.Application.Persistence.Contracts.Cloudinary;
using ComplaintSystem.Infrastructure.services.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Infrastructure.services
{
    public class ImaggaService : IImaggaService
    {
        private readonly ICloudinaryService _cloudinaryService;
        public ImaggaService(ICloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }
        public async Task<bool> AIGenerated(IFormFile image)
        {
            var file = await _cloudinaryService.UploadImageAsync(image);
            var client = new RestClient("https://api.sightengine.com/1.0");
            var request = new RestRequest("/check.json", Method.Get);
            // Add query parameters
            request.AddParameter("url", file.Link);
            request.AddParameter("models", "genai");
            request.AddParameter("api_user", Environment.GetEnvironmentVariable("SIGHT_ENGINE_API_USER"));
            request.AddParameter("api_secret", Environment.GetEnvironmentVariable("SIGHT_ENGINE_API_SECRET"));
            RestResponse response = await client.ExecuteAsync(request);
            bool flag = true;
            if (response.IsSuccessStatusCode)
            {
                SightEngineResponse sightEngineResponse = JsonConvert.DeserializeObject<SightEngineResponse>(response.Content);
                if (sightEngineResponse != null && sightEngineResponse.type.ai_generated >= 0.5)
                {
                    flag = false;
                }
            }
            await _cloudinaryService.DeleteFile(file.PublicId);

            return flag;

        }

        public async Task<AIdto> Check(IFormFile image)
        {
            // Define the base URL of the API endpoint
            string baseUrl = "https://api.aiornot.com/v1";

            string aiOrNotToken = Environment.GetEnvironmentVariable("AiOrNot_Token");
            var requestJSon = new AIorNotImageRequest
            {
                image = image
            };

            // Create a RestClient instance with the base URL
            var client = new RestClient(baseUrl);

            // Create a new RestRequest with the resource path and method
            var request = new RestRequest("/reports/image", Method.Post);

            // Add the email and password as parameters to the request
            request.AddJsonBody(requestJSon);

            //add header 
            request.AddHeader("Authorization", "Bearer " + aiOrNotToken);
            // Execute the request asynchronously
            var response = await client.ExecuteAsync(request);
            AIdto idto;
            if (response.IsSuccessStatusCode)
            {
                idto = JsonConvert.DeserializeObject<AIdto>(response.Content);
            }
            else
            {
                idto = new AIdto();
            }
            return idto;


        }

        public async Task<List<string>> Tagger(string image)
        {
            string apiSecret = Environment.GetEnvironmentVariable("Immaga_API_Secret");
            string apiKey = Environment.GetEnvironmentVariable("Immaga_API_Key");
            string basicAuthValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(String.Format("{0}:{1}", apiKey, apiSecret)));
            var client = new RestClient("https://api.imagga.com/v2/");
            var request = new RestRequest("tags", Method.Get);

            request.AddParameter("image_url", image);
            request.AddHeader("Authorization", String.Format("Basic {0}", basicAuthValue));

            RestResponse response = await client.ExecuteAsync(request);

            List<string> imageTags = new List<string>();
            if (response.IsSuccessStatusCode)
            {
                TaggerDto jsonResponse = JsonConvert.DeserializeObject<TaggerDto>(response.Content)!;
                foreach (var tags in jsonResponse.result.tags)
                {
                    if (tags.confidence >= 40)
                    {
                        imageTags.Add(tags.tag.en);

                    }
                }
            }

            return imageTags;
        }
    }
}
