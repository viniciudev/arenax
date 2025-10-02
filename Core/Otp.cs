using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Otp : BaseModel
    {

        [EmailAddress]
        [MaxLength(200)]
        public required string Email { get; set; }

    
        [MaxLength(500)]
        public required string HashedCode { get; set; }

      
        [MaxLength(50)]
        public required string Purpose { get; set; } // password_reset, email_verification, 2fa

        [Required]
        public DateTime ExpiryDate { get; set; }

        public int Attempts { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    
}
