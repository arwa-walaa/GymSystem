using GymSystemDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Repositroies.Interfaces
{
    public interface IPlanRepo
    {
        Plan? GetById(int id);
        IEnumerable<Plan> GetAll(Func<Plan, bool>? condtion = null);

        int Update(Plan plan);
    }
}
