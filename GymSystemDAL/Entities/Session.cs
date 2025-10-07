using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Entities
{
    internal class Session : BaseEntity
    {
        public string Description { get; set; } = null!;
        public int Capacity { get; set; }
        public DateTime StratDate { get; set; }
        public DateTime EndDate { get; set; }

        #region 1:M RS Between SessionCategory

        //FK
        public int CategoryId { get; set; }
        public Category SessionCategory { get; set; }

        #endregion

        #region 1:M RS Between SessionTrainer

        public Trainer SessionTrainer { get; set; }
        //fk
        public int TrainerId { get; set; }

        #endregion

        #region M:N RS Between MemberSession

        public ICollection<MemberSession> SessionMembers { get; set; }

        #endregion
    }
}
