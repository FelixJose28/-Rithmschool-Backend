using System;
using System.Collections.Generic;

#nullable disable

namespace Rithmschool.Core.DTOs
{
    public partial class BuyDTO
    {
        public int BuyId { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
    }
}
