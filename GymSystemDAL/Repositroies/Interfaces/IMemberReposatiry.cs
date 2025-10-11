using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Repositroies.Interfaces
{
    public interface IMemberReposatiry
    {
        IEnumerable<Entities.Member> GetAll();

        Entities.Member? GetById(int id);

        int Add(Entities.Member member);

        int Update(Entities.Member member);

        int Delete(int id);
    }
}
