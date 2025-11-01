using GymSystemBLL.Services.Interfaces;
using GymSystemBLL.ViewModels.AccountViewModel;
using GymSystemDAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymSystemPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController( IAccountService accountService ,SignInManager<ApplicationUser> signInManager )
        {
            _accountService = accountService;
           _signInManager = signInManager;
        }

        //login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel) {
        
            if (!ModelState.IsValid)
            {
                //ModelState.AddModelError("InvalidModel", "Please correct the errors and try again.");

                return View(loginViewModel);
            }
            var User = _accountService.ValidateUser(loginViewModel);
            if (User == null)
            {
                ModelState.AddModelError("InvalidCredentials", "The email or password you entered is incorrect.");
                return View(loginViewModel);
            }
            var result= _signInManager.PasswordSignInAsync(User, loginViewModel.Password, loginViewModel.RememberMe, false).Result;
          
           
            if (result.IsNotAllowed)
            {
                ModelState.AddModelError("SignInFailed", "Unable to sign in. Please try again later.");
                return View(loginViewModel);
            }
            if(result.IsLockedOut)
            {
                ModelState.AddModelError("AccountLocked", "Your account is locked. Please contact support.");
                return View(loginViewModel);
            }
            if(result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(loginViewModel);
        }



        //logout


        //access denied
    }
}
