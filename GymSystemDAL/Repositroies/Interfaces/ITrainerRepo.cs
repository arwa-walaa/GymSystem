using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Repositroies.Interfaces
{
    internal interface ITrainerRepo 
    {
        IEnumerable<Entities.Trainer> GetAll();
        Entities.Trainer? GetById(int id);
        int Add(Entities.Trainer trainer);
        int Update(Entities.Trainer trainer);
        int Delete(int id);

    }
}
