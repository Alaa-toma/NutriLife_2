using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.DTO.Request
{
    public class UploadProfileImageRequest
    {
        public IFormFile File { get; set; } = null!;
    }
}
