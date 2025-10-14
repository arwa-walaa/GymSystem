using GymSystemDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Repositroies.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepo<TEntity> GetRepo<TEntity>() where TEntity : BaseEntity,new();
       int SaveChanges();
    }
}
