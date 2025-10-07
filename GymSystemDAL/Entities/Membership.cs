using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Entities
{
    internal class Membership : BaseEntity
    {
      
       
        #region 1:M RS Between MembershipPlan
        //FK
        public int PlanId { get; set; }
        public Plan Plan { get; set; }
        #endregion
        #region 1:M RS Between MembershipMember
        //FK
        public int MemberId { get; set; }
        public Member Member { get; set; }
        #endregion

        public DateTime EndDate { get; set; }

        public string Status {
            get {
                if (DateTime.Now <= EndDate)
                {
                    return "Active";

                }
                else
                {
                    return "Expired!";
                   
                }
            }
            
          
        } 
    }
}
