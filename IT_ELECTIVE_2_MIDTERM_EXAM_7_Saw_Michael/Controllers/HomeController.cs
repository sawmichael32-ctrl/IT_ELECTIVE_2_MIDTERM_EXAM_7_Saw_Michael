using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IT_ELECTIVE_2_MIDTERM_EXAM_7_Saw_Michael.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "MemberVisit");
            }

            return RedirectToAction("Login", "Account");
        }
    }
}