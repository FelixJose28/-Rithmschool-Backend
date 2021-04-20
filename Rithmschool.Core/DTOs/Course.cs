using System;
using System.Collections.Generic;

#nullable disable

namespace Rithmschool.Core.DTOs
{
    public partial class Course
    {
        public Course()
        {
            Buys = new HashSet<Buy>();
        }

        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Route { get; set; }
        public double? Duration { get; set; }
        public double? Price { get; set; }
        public int? TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<Buy> Buys { get; set; }
    }
}
