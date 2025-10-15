using AutoMapper;
using GymSystemBLL.Services.Interfaces;
using GymSystemBLL.ViewModels.SessionsViewModel;
using GymSystemDAL.Entities;
using GymSystemDAL.Repositroies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Services.Clasess
{
    internal class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var sessions = _unitOfWork.SessionRepo.GetAllSessionWithTrainerAndCategory();
            if (!sessions.Any()) return [];
            return sessions.Select(s => new SessionViewModel
            {
                Id = s.Id,
                CategoryName = s.SessionCategory.CategoryName,
                Description = s.Description,
                StartDate = s.StratDate,
                EndDate = s.EndDate,
                TrainerName = $"{s.SessionTrainer.Name}",
                Capacity = s.Capacity,
                AvailableSlot = s.Capacity - _unitOfWork.SessionRepo.GetCountOfBookedSlots(s.Id)
            });


          
        }

        public SessionViewModel? GetSessionByID(int SessionId)
        {
            var session = _unitOfWork.SessionRepo.GetSessionWithTrainerAndCategoryByID(SessionId);
            if (session == null) return null;
            //return new SessionViewModel
            //{
            //    Id = session.Id,
            //    CategoryName = session.SessionCategory.CategoryName,
            //    Description = session.Description,
            //    StartDate = session.StratDate,
            //    EndDate = session.EndDate,
            //    TrainerName = $"{session.SessionTrainer.Name}",
            //    Capacity = session.Capacity,
            //    AvailableSlot = session.Capacity - _unitOfWork.SessionRepo.GetCountOfBookedSlots(session.Id)
            //};
            var MappedSession= _mapper.Map<Session,SessionViewModel>(session);
            MappedSession.AvailableSlot = MappedSession.Capacity - _unitOfWork.SessionRepo.GetCountOfBookedSlots(session.Id);
            return MappedSession;

        }
    }
}
