using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.DTO.Response
{
    public class InBodyPreviewResponse
    {
        
        public string TempFileId { get; set; } = string.Empty;

        // All values Gemini extracted
        public InBodyExtractedData ExtractedData { get; set; } = new();

        // Fields where AI confidence was below 75% — highlight these on frontend
        public List<string> LowConfidenceFields { get; set; } = new();

        // Overall confidence 0.0 to 1.0
        public float ConfidenceScore { get; set; }

        // Human-readable message for the user
        public string ConfidenceMessage => ConfidenceScore switch
        {
            >= 0.90f => "AI extracted data with high confidence.",
            >= 0.75f => "Most fields extracted successfully. Please review highlighted fields.",
            >= 0.50f => "Low confidence. Please carefully review all values before confirming.",
            _ => "Very low confidence. Consider uploading a clearer image."
        };
    }
}
