using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IImageUrlRepository
    {
        string GetImageUrl(string fileName);
        List<string> GetImageUrls(IEnumerable<string> fileNames);
    }
    public class ImageUrlRepository : IImageUrlRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ImageUrlRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetImageUrl(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return string.Empty;

            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null)
                return string.Empty;

            // Remove qualquer barra inicial para evitar dupla barra
            var cleanFileName = fileName.TrimStart('/');

            return $"{request.Scheme}://{request.Host}{request.PathBase}/api/sportscourt/GetByUniqueName/{cleanFileName}";
        }

        public List<string> GetImageUrls(IEnumerable<string> fileNames)
        {
            if (fileNames == null)
                return new List<string>();

            return fileNames
                .Where(fileName => !string.IsNullOrEmpty(fileName))
                .Select(GetImageUrl)
                .ToList();
        }
    }
}
