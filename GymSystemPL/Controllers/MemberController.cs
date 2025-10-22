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

        public ActionResult MemberEdit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Member Id.";
                return RedirectToAction(nameof(Index));
            }
            var memberToUpdate = _memberService.GetMemberForUpdate(id);
            if (memberToUpdate == null)
            {
                TempData["ErrorMessage"] = "Member not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(memberToUpdate);

        }
        [HttpPost]
        public ActionResult MemberEdit([FromRoute]int id, MamberToUpdateViewModel mamberToUpdate)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Date invalied", "Check data And Missng Fields.");
                return View(  mamberToUpdate);
            }
            bool result = _memberService.UpdateMemberDetails(id, mamberToUpdate);
            if (result)
            {
                TempData["SuccessMessage"] = "Member updated successfully.";
                //return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update member.";
                //return RedirectToAction(nameof(MemberEdit), new { id = id });
            }
            return RedirectToAction(nameof(Index));

        }
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Member Id.";
                return RedirectToAction(nameof(Index));
            }


            var member = _memberService.GetMemberDetails(id);
            if (member == null)
            {
                TempData["ErrorMessage"] = "member not found";
                return RedirectToAction(nameof(Index));

            }
            ViewBag.MemberName = member.Name;
            ViewBag.MemberId = member.Id;
            return View();
        }

        [HttpPost]
        public ActionResult DeleteConfirmed([FromForm]int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Member Id.";
                return RedirectToAction(nameof(Index));
            }
            bool result = _memberService.DeleteMember(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Member deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete member.";
            }
            return RedirectToAction(nameof(Index));
        }
        }
    }
