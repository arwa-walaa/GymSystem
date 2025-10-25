using GymSystemBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymSystemPL.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
           _planService = planService;
        }
        public IActionResult Index()
        {
            var plans = _planService.GetAllPlans();

            return View(plans);
        }
        public IActionResult Details(int id) {
           if(id <= 0)
            {
               TempData["ErrorMessage"] = "Invalid Plan Id.";
                return RedirectToAction(nameof(Index));
            }
            var plan = _planService.GetPlanById(id);
            if (plan == null)
            {
                TempData["ErrorMessage"] = "Plan not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(plan);



        }

    }
}
