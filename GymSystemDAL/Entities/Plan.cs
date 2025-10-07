using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Entities
{
    internal class Plan : BaseEntity
    {

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int DurationDays { get; set; }

        public bool IsActive { get; set; }

        #region M:N RS Between MemberPlan

        public ICollection<Membership> Plans { get; set; }

        #endregion

    }
}
