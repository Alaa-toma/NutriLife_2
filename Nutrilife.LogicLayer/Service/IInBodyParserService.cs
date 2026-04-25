using Microsoft.AspNetCore.Http;
using Nutrilife.DataAccessLayer.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.LogicLayer.Service
{
    public interface IInBodyParserService
    {
        Task<InBodyPreviewResponse?> ParseFromFileAsync(IFormFile file);
    }
}
