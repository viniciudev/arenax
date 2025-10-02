using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class FirebaseNotification
    {
        public string? Title { get; set; }
        public string ?Body { get; set; }
        public string ?ImageUrl { get; set; }
        public Dictionary<string, string>? Data { get; set; } = new Dictionary<string, string>();
    }
    public class NotificationRequest
    {
        public string? DeviceToken { get; set; }
        public string ?Topic { get; set; }
        public int? IdClient { get; set; }
        public FirebaseNotification? Notification { get; set; }
    }

    public class NotificationResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? ResponseId { get; set; }
    }
}
