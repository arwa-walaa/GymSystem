using GymSystemDAL.Data.Context;
using GymSystemDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Repositroies.Classes
{
    internal class TrainerRepo : Interfaces.ITrainerRepo
    {
        private readonly GymSystemDBContext _dbContext;

        public TrainerRepo( GymSystemDBContext dbContext  )
        {
            _dbContext = dbContext;
        }
        public int Add(Trainer trainer)
        {
          _dbContext.Trainers.Add(trainer);
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            
            var trainer = _dbContext.Trainers.Find(id);
            if (trainer is null)
            {
                return 0;
            }
            _dbContext.Trainers.Remove(trainer);
            return _dbContext.SaveChanges();

        }

        public IEnumerable<Trainer> GetAll()
        {
            
            return _dbContext.Trainers.ToList();

        }

        public Trainer? GetById(int id)
        {
            
            return _dbContext.Trainers.Find(id);
        }

        public int Update(Trainer trainer)
        {
            
            _dbContext.Trainers.Update(trainer);
            return _dbContext.SaveChanges();
        }
    }
}
