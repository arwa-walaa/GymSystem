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
        public IActionResult Index()
        {
            var members = _memberService.GetAllMembers();
            return View(members);
        }
    }
}
