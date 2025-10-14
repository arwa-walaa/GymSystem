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
        public void Add(TEntity entity)
        {
            _dBContext.Set<TEntity>().Add(entity);
           


        }

        public void Delete(TEntity entity)
        {
           
         
            _dBContext.Set<TEntity>().Remove(entity);
          
        }

        public IEnumerable<TEntity> GetAll(Func<TEntity, bool>? condtion = null)
        {
            if (condtion is not null)
            {
                return _dBContext.Set<TEntity>().Where(condtion).ToList();
            }
            else
            { 
                return _dBContext.Set<TEntity>().ToList();
            }
        }

        public TEntity? GetById(int id)
        {
            
            return _dBContext.Set<TEntity>().Find(id);
        }

        public void Update(TEntity entity)
        {
           
            _dBContext.Set<TEntity>().Update(entity);
          
        }

      
    }
}
