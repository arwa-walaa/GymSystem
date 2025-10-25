using GymSystemBLL.Services.Interfaces;
using GymSystemBLL.ViewModels;
using GymSystemDAL.Entities;
using GymSystemDAL.Repositroies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Services.Clasess
{
    public class AnaliticsService : IAnaliticsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnaliticsService(IUnitOfWork  unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public AnaliticsViewModel GetAnaliticsData()
        {
            var sessionRepo = _unitOfWork.SessionRepo.GetAll();
            return new AnaliticsViewModel {
                TotalMembers = _unitOfWork.GetRepo<Member>().GetAll().Count(),
                TotalTrainers = _unitOfWork.GetRepo<Trainer>().GetAll().Count(),
                ActiveMembers = _unitOfWork.GetRepo<Membership>().GetAll(X=>X.Status=="Active").Count(),
                UpComingSessions = sessionRepo.Count(s => s.StratDate > DateTime.Now),
                OnGoingSessions = sessionRepo.Count(s => s.StratDate <= DateTime.Now && s.EndDate >= DateTime.Now),
                CompletedSessions = sessionRepo.Count(s => s.EndDate < DateTime.Now)
            };

        }
    }
}
