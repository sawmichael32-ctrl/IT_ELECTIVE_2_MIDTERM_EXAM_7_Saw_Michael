using System.Security.Claims;
using IT_ELECTIVE_2_MIDTERM_EXAM_7_Saw_Michael.Models;
using IT_ELECTIVE_2_MIDTERM_EXAM_7_Saw_Michael.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IT_ELECTIVE_2_MIDTERM_EXAM_7_Saw_Michael.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserRepository _userRepository;

        public AccountController()
        {
            _userRepository = new UserRepository();
        }

        // GET: /Account/Login
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError(
                    "",
                    "Username and password are required."
                );

                return View();
            }

            var user = _userRepository.ValidateLogin(
                username,
                password
            );

            if (user == null)
            {
                ModelState.AddModelError(
                    "",
                    "Invalid username or password."
                );

                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.Id.ToString()
                ),

                new Claim(
                    ClaimTypes.Name,
                    user.Username
                ),

                new Claim(
                    ClaimTypes.Email,
                    user.Email
                ),

                new Claim(
                    "FullName",
                    $"{user.FirstName} {user.LastName}"
                )
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal
            );

            return RedirectToAction(
                "Index",
                "MemberVisit"
            );
        }

        // GET: /Account/Register
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var existingUser =
                _userRepository.GetByUsername(user.Username);

            if (existingUser != null)
            {
                ModelState.AddModelError(
                    "Username",
                    "Username is already registered."
                );

                return View(user);
            }

            _userRepository.Add(user);

            TempData["SuccessMessage"] =
                "Registration successful. You can now log in.";

            return RedirectToAction(nameof(Login));
        }

        // POST: /Account/Logout
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            return RedirectToAction(nameof(Login));
        }
    }
}