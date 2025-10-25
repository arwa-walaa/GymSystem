using GymSystemBLL.Services.Interfaces;
using GymSystemBLL.ViewModels.SessionsViewModel;
using GymSystemDAL.Repositroies.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymSystemPL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;
   

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
           
        }
        public IActionResult Index()
        {
            var sessions = _sessionService.GetAllSessions();
            return View(sessions);
        }

        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id.";
                return RedirectToAction(nameof(Index));
            }
            var session = _sessionService.GetSessionByID(id);
            if (session == null)
            {
                TempData["ErrorMessage"] = "Session not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(session);
        }

        public IActionResult Create()
        {
            // Get categories and trainers for dropdowns
          
            PopulateDropdownsForCategory();
            PopulateDropdownsForTrainer();



            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateSessionViewModel createSessionViewModel)
        {
            if (!ModelState.IsValid)
            {
                // Re-populate dropdowns on error
                PopulateDropdownsForCategory();
                PopulateDropdownsForTrainer();

                ModelState.AddModelError("", "Please correct the errors and try again.");
                return View(createSessionViewModel);
            }

            var result = _sessionService.CreateSession(createSessionViewModel);
            if (!result)
            {
                // Re-populate dropdowns on error
                PopulateDropdownsForCategory();
                PopulateDropdownsForTrainer();

                TempData["ErrorMessage"] = "Failed to create session. Please check your data and try again.";
                return View(createSessionViewModel);
            }

            TempData["SuccessMessage"] = "Session created successfully.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id.";
                return RedirectToAction(nameof(Index));
            }

            var session = _sessionService.GetSessionToUpdate(id);
            if (session == null)
            {
                TempData["ErrorMessage"] = "Session not found or cannot be updated.";
                return RedirectToAction(nameof(Index));
            }

            // Populate dropdowns for the edit form
            PopulateDropdownsForTrainer();

            return View(session);
        }

        [HttpPost]
        public IActionResult Edit(int id, UpdateSessionViewModel updateSessionViewModel)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id.";
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                PopulateDropdownsForTrainer();
                ModelState.AddModelError("", "Please correct the errors and try again.");
                return View(updateSessionViewModel);
            } 

            var result = _sessionService.UpdateSession(id, updateSessionViewModel);
            if (!result)
            {
                PopulateDropdownsForTrainer();
                TempData["ErrorMessage"] = "Failed to update session. Please check your data and try again.";
                return View(updateSessionViewModel);
            }

            TempData["SuccessMessage"] = "Session updated successfully.";
            return RedirectToAction(nameof(Index));
        }
        private void PopulateDropdownsForCategory()
        {
            var categories = _sessionService.GetCategoryForSesstions();
           
            ViewBag.categories = new SelectList(categories, "Id", "Name");
        }

        private void PopulateDropdownsForTrainer()
        {
            var trainers = _sessionService.GetTrainerForSesstions();
            ViewBag.Trainers = new SelectList(trainers, "Id", "Name");
        }
    }
}
