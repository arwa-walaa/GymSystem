using GymSystemBLL.ViewModels;
using GymSystemBLL.ViewModels.PlanViewModel;
using GymSystemDAL.Entities;
using GymSystemDAL.Repositroies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        public UpdatePlanViewModel? GetPlanToUpdate(int planId)
        {
            var plan = _unitOfWork.GetRepo<GymSystemDAL.Entities.Plan>().GetById(planId);
            if (plan is null || plan.IsActive==false || HasActiveMemberShip(planId) ) return null;
            return new UpdatePlanViewModel()
            {
                PlanName = plan.Name,
                Description = plan.Description,
                Price = plan.Price,
                DurationDays = plan.DurationDays,

            };
        
        }

        public bool ToggleStatus(int PlanId)
        {


            var plan = _unitOfWork.GetRepo<Plan>().GetById(PlanId);
            //Check Active Memberships with this Plan
            if (plan is null || HasActiveMemberShip(PlanId)) return false;
            try
            {
              
                plan.IsActive = plan.IsActive==true ? false : true;
                plan.UpdatedAt=DateTime.Now;
                _unitOfWork.GetRepo<Plan>().Update(plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdatePlan(int planId, UpdatePlanViewModel updatedPlan)
        {
            var plan = _unitOfWork.GetRepo<GymSystemDAL.Entities.Plan>().GetById(planId);
            if (plan is null || HasActiveMemberShip(planId)) return false;
            try
            {
                (plan.Name, plan.Description, plan.Price, plan.DurationDays) =
         (updatedPlan.PlanName, updatedPlan.Description, updatedPlan.Price, updatedPlan.DurationDays);
                _unitOfWork.GetRepo<Plan>().Update(plan);
                return _unitOfWork.SaveChanges()>0;
            }
            catch (Exception) {
            
                return false;
            }
        }

        #region Helper

        private bool HasActiveMemberShip(int PlanId)
        {
            var ActiveMembership= _unitOfWork.GetRepo<GymSystemDAL.Entities.Membership>().GetAll(P=>P.PlanId == PlanId && P.Status=="Active" );
            return ActiveMembership.Any();
        }


        #endregion
    }
}
