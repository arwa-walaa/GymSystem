using GymSystemBLL.Services.Interfaces;
using GymSystemBLL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymSystemPL.Controllers
{
    [Authorize(Roles = "Super Admin")]
    public class TrainerController : Controller
    {
        private readonly ITrainerService _trainerService;

        //ask CLR to inject the service
        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        #region Get All Trainers
        public IActionResult Index()
        {
            var trainers = _trainerService.GetAllTrainers();
            return View(trainers);
        }
        #endregion

        #region Get trainer details 
        public ActionResult TrainerDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer Id.";
                return RedirectToAction(nameof(Index));
            }
            var trainerDetails = _trainerService.GetTrainerDetails(id);
            if (trainerDetails == null)
            {
                TempData["ErrorMessage"] = "Trainer not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(trainerDetails);
        }
        #endregion

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Store(CreateTrainerViewModel createTrainer)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Date invalid", "Check data And Missing Fields.");
                return View(nameof(Create), createTrainer);
            }

            bool result = _trainerService.CreateTrainer(createTrainer);
            if (result)
            {
                TempData["SuccessMessage"] = "Trainer created successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to create trainer. Email or Phone may already exist.";
                return RedirectToAction(nameof(Create));
            }
        }

        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer Id.";
                return RedirectToAction(nameof(Index));
            }
            var trainerToUpdate = _trainerService.GetTrainerToUpdate(id);
            if (trainerToUpdate == null)
            {
                TempData["ErrorMessage"] = "Trainer not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(trainerToUpdate);
        }

        [HttpPost]
        public ActionResult Edit([FromRoute] int id, TrainerToUpdateViewModel trainerToUpdate)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Date invalid", "Check data And Missing Fields.");
                return View(trainerToUpdate);
            }
            bool result = _trainerService.UpdateTrainerDetails(trainerToUpdate, id);
            if (result)
            {
                TempData["SuccessMessage"] = "Trainer updated successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update trainer.";
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer Id.";
                return RedirectToAction(nameof(Index));
            }

            var trainer = _trainerService.GetTrainerDetails(id);
            if (trainer == null)
            {
                TempData["ErrorMessage"] = "Trainer not found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.TrainerName = trainer.Name;
            ViewBag.TrainerId = trainer.Id;
            return View();
        }

        [HttpPost]
        public ActionResult DeleteConfirmed([FromForm] int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer Id.";
                return RedirectToAction(nameof(Index));
            }
            bool result = _trainerService.RemoveTrainer(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Trainer deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete trainer.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
