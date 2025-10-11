using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Entities
{
    public class Member : GymUser
    {
      public string? Photo { get; set; }

        #region 1:1 RS Between Member HealthRecord

        public HealthRecord HealthRecord { get; set; }
        #endregion

        #region M:N RS Between MemberPlan

        public ICollection<Membership> Memberships { get; set; }

        #endregion

        #region M:N RS Between MemberSession

        public ICollection<MemberSession> MemberSessions { get; set; }

        #endregion


    }
}
