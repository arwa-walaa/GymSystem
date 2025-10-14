using GymSystemBLL.ViewModels.PlanViewModel;
using GymSystemDAL.Entities;
using GymSystemDAL.Repositroies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Services.Clasess
{
    public class PlanService : Interfaces.IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlanService( IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var plans = _unitOfWork.GetRepo<GymSystemDAL.Entities.Plan>().GetAll();
            if (plans is null || !plans.Any())
            {
                return[];
            }

            return plans.Select(plan=> new PlanViewModel
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                Price = plan.Price,
                DurationInDays = plan.DurationDays,
                IsActive = plan.IsActive,
            });
        }

        public PlanViewModel? GetPlanById(int id)
        {
            var plan = _unitOfWork.GetRepo<GymSystemDAL.Entities.Plan>().GetById(id);
            if (plan is null) return null;
            return new PlanViewModel
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                Price = plan.Price,
                DurationInDays = plan.DurationDays,
                IsActive = plan.IsActive,
            };
        }
    }
}
