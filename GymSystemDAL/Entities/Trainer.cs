using GymSystemDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Entities
{
    public class Trainer : GymUser
    {
        public Trainer()
        {
            TrainerSessions = new List<Session>();
        }
      
        public Specialist Specialites { get; set; }

        #region 1:M RS Between SessionTrainer

        public  ICollection<Session> TrainerSessions { get; set; }

        #endregion

    }
}
