using ComplaintSystem.Application.DTOs.RESTAPIDto;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Persistence.Contracts.APIs
{
    public interface IImaggaService
    {
        public Task<List<string>> Tagger(string images);
        public Task<AIdto> Check(IFormFile image);
    }
}
