using GymSystemDAL.Data.Context;
using GymSystemDAL.Entities;
using GymSystemDAL.Repositroies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Repositroies.Classes
{
    public class UnitOfWork : Interfaces.IUnitOfWork
    {
        private readonly GymSystemDBContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public UnitOfWork(GymSystemDBContext dbContext )
        {
            _dbContext = dbContext;
        }
        public IGenericRepo<TEntity> GetRepo<TEntity>() where TEntity : BaseEntity, new()
        {
           var EntityType= typeof(TEntity);
            if(_repositories.TryGetValue(EntityType,out var Repo))
            {
                return (IGenericRepo<TEntity>)Repo;
            }
            var NewRepo = new GenericRepo<TEntity>(_dbContext);
            _repositories[EntityType]=NewRepo;
            return NewRepo;

        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
