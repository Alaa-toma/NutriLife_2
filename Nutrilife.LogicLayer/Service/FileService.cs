using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.LogicLayer.Service
{
    public class FileService :IFileService
    {

        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        async Task<string?> IFileService.UploadAsync(IFormFile file)
        {

            if (file == null || file.Length == 0)
                return null;

            if (file.Length > 2 * 1024 * 1024)
            {
                throw new Exception("The image size is larger than allowed.");
            }

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName).ToLower()}";

            
            var webRoot = _environment.WebRootPath
                ?? Path.Combine(_environment.ContentRootPath, "wwwroot");

            var folderPath = Path.Combine(webRoot, "images", "profiles");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, fileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return fileName;
        }


        public Task<bool> DeleteAsync(string? fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return Task.FromResult(false);
            }

            var filePath = Path.Combine(_environment.WebRootPath, "images", "profiles", fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return Task.FromResult(false);
            }

            System.IO.File.Delete(filePath);
            return Task.FromResult(true);
        }

    }
}
