//using DatabaseContext.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using Web.Models;

//namespace Web.Controllers
//{
//    [AllowAnonymous]
//    public class AccountController : Controller
//    {
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly SignInManager<ApplicationUser> _signInManager;
//        public AccountController(UserManager<ApplicationUser> userManager,
//            SignInManager<ApplicationUser> signInManager)
//        {
//            this._userManager = userManager;
//            this._signInManager = signInManager;
//        }
//        public IActionResult Register()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Register(UserViewModel model)
//        {
//            if(ModelState.IsValid)
//            {
//                var result = _userManager.CreateAsync(new ApplicationUser
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Email = model.Email,
//                    UserName = model.UserName,
//                    EmailConfirmed = true,
//                    NormalizedUserName = model.UserName.ToUpper(),
//                    NormalizedEmail = model.Email.ToUpper(),
//                }, model.Password).Result;
//                if (result.Succeeded)
//                   return RedirectToAction("Login");
//                return View(model);
//            }
//            return View();
//        }

//        public IActionResult Login()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Login(LoginViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = _userManager.FindByEmailAsync(model.Email).Result;
//                if(user != null)
//                {
//                    var result = _signInManager.PasswordSignInAsync(user, model.Password,
//                        true, false).Result;
//                    if (result.Succeeded)
//                        return RedirectToAction("Index", "Home");
//                }
//                return View(model);
//            }
//            return View();
//        }


//        public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
//        {
//            var users = new[]
//            {
//        new { Email = "johndoe@example.com", Password = "Password123!" },
//    };

//            foreach (var user in users)
//            {
//                var applicationUser = new ApplicationUser
//                {
//                    Email = user.Email,
//                    UserName = user.Email,
                  
//                };

//                var result = await userManager.CreateAsync(applicationUser, user.Password);

//                if (!result.Succeeded)
//                {
//                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
//                }
//            }
//        }
//    }
//}
