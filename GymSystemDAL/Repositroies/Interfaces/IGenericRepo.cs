using GymSystemDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Repositroies.Interfaces
{
    public interface IGenericRepo<TEntity> where TEntity : BaseEntity , new()
    {
        IEnumerable<TEntity> GetAll( Func<TEntity,bool> ?condtion=null );
        TEntity? GetById(int id);
        int Add(TEntity entity);
        int Update(TEntity entity);
        int Delete(int id);

    }
}
