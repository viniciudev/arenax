﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class ResponseData
    {
        
            public bool Success { get; set; }
            public string? Message { get; set; }
            public object? Data { get; set; }
        
    }
}
