using GymSystemBLL.ViewModels;
using GymSystemBLL.ViewModels.PlanViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Services.Interfaces
{
    public interface IPlanService
    {
       
        IEnumerable<ViewModels.PlanViewModel.PlanViewModel> GetAllPlans();
        PlanViewModel? GetPlanById(int id);

        UpdatePlanViewModel? GetPlanToUpdate(int planId);

        bool UpdatePlan(int planId, UpdatePlanViewModel updatePlanViewModel);


    }
}
