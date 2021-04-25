using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

#nullable disable

namespace Rithmschool.Core.DTOs
{
    public partial class CourseDTO
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public IFormFile File { get; set; }
        public string Route { get; set; }
        public double? Duration { get; set; }
        public double? Price { get; set; }
        public int? TeacherId { get; set; }

    }
}
