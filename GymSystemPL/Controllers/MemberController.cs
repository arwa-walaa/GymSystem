using Microsoft.AspNetCore.Mvc;
using GymSystemBLL.Services.Interfaces;
using GymSystemBLL.ViewModels;
namespace GymSystemPL.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        //ask CLR to inject the service
        public MemberController(IMemberService memberService )
        {
            _memberService = memberService;
        }
        //register for the service in program.cs
        #region Get All Members
        public IActionResult Index()
        {
            var members = _memberService.GetAllMembers();
            return View(members);
        }

        #endregion

        #region Get member details 
        public ActionResult MemberDetials(int id)
        {
            if (id <= 0) { 
                TempData["ErrorMessage"] = "Invalid Member Id.";
                return RedirectToAction(nameof(Index)); 
             }
            var memberDetails = _memberService.GetMemberDetails(id);
            if (memberDetails == null)
            {
                TempData["ErrorMessage"] = "Member not found.";
                return RedirectToAction(nameof(Index));
            }
               
            return View(memberDetails);


        }
        #endregion
        public ActionResult HealthRecordDeatils(int id)
        {
            if (id <= 0) { 
                TempData["ErrorMessage"] = "Invalid Member Id.";
                return RedirectToAction(nameof(Index)); 
            
            }
            var HealthRecord = _memberService.GetMemberRecordHealth(id);
            if (HealthRecord == null)
            { 
                TempData["ErrorMessage"] = "Health Record not found.";
                return RedirectToAction(nameof(Index)); 
            
            }
            return View(HealthRecord);


        }
        public ActionResult Create()
        {
               return View();
        }
        [HttpPost]
        public ActionResult Store(CreateMemberViewModel createMember)
        {
            if (!ModelState.IsValid) { 
                ModelState.AddModelError("Date invalied", "Check data And Missng Fields.");
               
                return View(nameof(Create), createMember);
            }

            bool result= _memberService.CreateMember(createMember);
            if (result)
            {
                TempData["SuccessMessage"] = "Member created successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to create member. Email or Phone may already exist.";
                return RedirectToAction(nameof(Create));
            }
        }


    }
}
