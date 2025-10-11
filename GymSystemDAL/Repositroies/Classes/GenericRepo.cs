using GymSystemDAL.Data.Context;
using GymSystemDAL.Repositroies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Repositroies.Classes
{
    public class GenericRepo<TEntity> : IGenericRepo<TEntity> where TEntity : Entities.BaseEntity, new()
    {
        private readonly GymSystemDBContext _dBContext;

        public GenericRepo(GymSystemDBContext dBContext) {
           _dBContext = dBContext;
        }
        public int Add(TEntity entity)
        {
            _dBContext.Set<TEntity>().Add(entity);
            return _dBContext.SaveChanges();



        }

        public int Delete(int id)
        {
           
            var entity = _dBContext.Set<TEntity>().Find(id);
            if (entity is null)
            {
                return 0;
            }
            _dBContext.Set<TEntity>().Remove(entity);
            return _dBContext.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll()
        {
            
            return _dBContext.Set<TEntity>().ToList();
        }

        public TEntity? GetById(int id)
        {
            
            return _dBContext.Set<TEntity>().Find(id);
        }

        public int Update(TEntity entity)
        {
           
            _dBContext.Set<TEntity>().Update(entity);
            return _dBContext.SaveChanges();
        }

      
    }
}
