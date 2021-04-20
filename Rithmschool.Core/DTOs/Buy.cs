using System;
using System.Collections.Generic;

#nullable disable

namespace Rithmschool.Core.DTOs
{
    public partial class Buy
    {
        public int BuyId { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
        public virtual User User { get; set; }
    }
}
