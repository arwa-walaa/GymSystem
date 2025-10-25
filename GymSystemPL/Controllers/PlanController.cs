using GymSystemBLL.Services.Interfaces;
using GymSystemBLL.ViewModels;

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

        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Plan Id.";
                return RedirectToAction(nameof(Index));
            }
            var plan = _planService.GetPlanToUpdate(id);
            if (plan == null)
            {
                TempData["ErrorMessage"] = "Plan not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }
        [HttpPost]
        public ActionResult Edit([FromRoute] int id, UpdatePlanViewModel planViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("wrong Data", "Please correct the errors and try again.");
                return View(planViewModel);
            }
            var updated = _planService.UpdatePlan(id, planViewModel);
            if (!updated)
            {
                TempData["ErrorMessage"] = "Failed to update the plan.";
                return RedirectToAction(nameof(Index));
            }
            TempData["SuccessMessage"] = "Plan updated successfully.";
            return RedirectToAction(nameof(Index));
        }

    }
}
