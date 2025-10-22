using Microsoft.AspNetCore.Mvc;
using GymSystemBLL.Services.Interfaces;
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

    }
}
