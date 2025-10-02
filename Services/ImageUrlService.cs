using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IImageUrlService
    {
        string GetImageUrl(string fileName);
        List<string> GetImageUrls(List<string> fileNames);
    }

    public class ImageUrlService : IImageUrlService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ImageUrlService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetImageUrl(string fileName)
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null) return string.Empty;

            return $"{request.Scheme}://{request.Host}{request.PathBase}/api/Image/GetByUniqueName/{fileName}";
        }

        public List<string> GetImageUrls(List<string> fileNames)
        {
            return fileNames.Select(GetImageUrl).ToList();
        }
    }
}
