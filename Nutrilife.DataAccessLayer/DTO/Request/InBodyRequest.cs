using Microsoft.AspNetCore.Http;
using Nutrilife.DataAccessLayer.DTO.Response;
using Nutrilife.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.DTO.Request
{
    public class InBodyRequest
    {
        public IFormFile file { get; set; }
        public string? TempFileId { get; set; } = string.Empty;
        public int HealthTrackingId { get; set; }
        public role creatorRole { get; set; }
        public InBodyDataEditRequest ConfirmedData { get; set; } = new();

    }
}
