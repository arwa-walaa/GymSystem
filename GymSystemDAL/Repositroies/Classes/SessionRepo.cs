using GymSystemDAL.Data.Context;
using GymSystemDAL.Entities;
using GymSystemDAL.Repositroies.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Repositroies.Classes
{
    public class SessionRepo :GenericRepo<Session>, ISessionRepo
    {
        private readonly GymSystemDBContext _dBContext;

        public SessionRepo(Data.Context.GymSystemDBContext dBContext) : base(dBContext)
        {
           _dBContext = dBContext;
        }

   

        public IEnumerable<Session> GetAllSessionWithTrainerAndCategory()
        {
            return _dBContext.Sessions
                .Include(X=>X.SessionTrainer)
                .Include(X=>X.SessionCategory)
                .ToList();

        }

     
        public int GetCountOfBookedSlots(int sessionID)
        {
            return _dBContext.MemberSessions.Count(X=>X.SessionId== sessionID);

        }
        public Session? GetSessionWithTrainerAndCategoryByID(int sessionID)
        {
            return _dBContext.Sessions
                .Include(X => X.SessionTrainer)
                .Include(X => X.SessionCategory)
                .FirstOrDefault(X=>X.Id==sessionID);

        }


    }
}
