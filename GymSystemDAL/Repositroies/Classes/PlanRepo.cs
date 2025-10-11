using GymSystemDAL.Data.Context;
using GymSystemDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Repositroies.Classes
{
    public class PlanRepo : Interfaces.IPlanRepo
    {
        private readonly GymSystemDBContext _dBContext;

        public PlanRepo(GymSystemDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public IEnumerable<Plan> GetAll(Func<Plan, bool>? condtion = null)
        {
            if (condtion is not null)
            {
                return _dBContext.Plans.Where(condtion).ToList();
            }
            else
            {
                return _dBContext.Plans.ToList();
            }

        }

        public Plan? GetById(int id)
        {
            
            return _dBContext.Plans.Find(id);
        }

        public int Update(Plan plan)
        {
           _dBContext.Plans.Update(plan);
            return _dBContext.SaveChanges();


        }
    }
}
