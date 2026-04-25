using Microsoft.AspNetCore.Http;
using Nutrilife.DataAccessLayer.DTO.Request;
using Nutrilife.DataAccessLayer.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.LogicLayer.Service
{
    public interface IHealthTrackingService
    {
        Task<MessageResponse> CreateSessionAsync(HealthTrackingRequest request);

        Task<InBodyPreviewResponse> PreviewInBodyAsync(InBodyRequest request);


        Task<HealthTrackingResponse> ConfirmInBodyAsync(InBodyRequest request);

        Task<ManualMeasurementResponse> AddManualMeasurementAsync(ManualMeasurementRequest request);


        Task<ProgressResponse> GetProgressAsync(string clientId);
        
    }


}
