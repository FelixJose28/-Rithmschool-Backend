using System;
using System.Collections.Generic;

#nullable disable

namespace Rithmschool.Core.DTOs
{
    public partial class Teacher
    {
        public Teacher()
        {
            Courses = new HashSet<Course>();
        }

        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Skills { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
