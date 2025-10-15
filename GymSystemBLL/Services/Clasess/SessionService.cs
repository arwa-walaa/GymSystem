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

        public SessionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
    }
}
