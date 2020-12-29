using System;
using System.Collections.Generic;
using System.Text;

namespace EnvRnk.Services.DTOs.Point
{
    public class PointForArticleDetails
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public bool Plus { get; set; }
    }
}
