using IT_ELECTIVE_2_MIDTERM_EXAM_7_Saw_Michael.Models;
using IT_ELECTIVE_2_MIDTERM_EXAM_7_Saw_Michael.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IT_ELECTIVE_2_MIDTERM_EXAM_7_Saw_Michael.Controllers
{
    [Authorize]
    public class MemberVisitController : Controller
    {
        private readonly MemberVisitRepository _repository;

        public MemberVisitController(MemberVisitRepository repository)
        {
            _repository = repository;
        }

        // GET: /MemberVisit
        public IActionResult Index(string? searchString)
        {
            var visits = _repository.GetAll();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                visits = visits
                    .Where(v =>
                        v.VisitNumber.Contains(
                            searchString,
                            StringComparison.OrdinalIgnoreCase)

                        || v.MemberId.Contains(
                            searchString,
                            StringComparison.OrdinalIgnoreCase)

                        || v.FirstName.Contains(
                            searchString,
                            StringComparison.OrdinalIgnoreCase)

                        || v.LastName.Contains(
                            searchString,
                            StringComparison.OrdinalIgnoreCase)

                        || v.MembershipType.Contains(
                            searchString,
                            StringComparison.OrdinalIgnoreCase)
                    )
                    .ToList();
            }

            ViewBag.SearchString = searchString;

            return View(visits);
        }

        // GET: /MemberVisit/Create
        [HttpGet]
        public IActionResult Create()
        {
            var visit = new MemberVisit
            {
                VisitDate = DateTime.Today,
                TimeIn = DateTime.Now,
                Status = "Inside Gym"
            };

            return View(visit);
        }

        // POST: /MemberVisit/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MemberVisit visit)
        {
            if (!ModelState.IsValid)
            {
                return View(visit);
            }

            visit.Status = "Inside Gym";
            visit.TimeOut = null;

            _repository.Add(visit);

            TempData["SuccessMessage"] =
                "Member visit successfully registered.";

            return RedirectToAction(nameof(Index));
        }

        // GET: /MemberVisit/Edit/1
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var visit = _repository.GetById(id);

            if (visit == null)
            {
                return NotFound();
            }

            return View(visit);
        }

        // POST: /MemberVisit/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, MemberVisit visit)
        {
            if (id != visit.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(visit);
            }

            _repository.Update(visit);

            TempData["SuccessMessage"] =
                "Member visit successfully updated.";

            return RedirectToAction(nameof(Index));
        }

        // GET: /MemberVisit/Details/1
        [HttpGet]
        public IActionResult Details(int id)
        {
            var visit = _repository.GetById(id);

            if (visit == null)
            {
                return NotFound();
            }

            return View(visit);
        }

        // GET: /MemberVisit/CheckOut/1
        [HttpGet]
        public IActionResult CheckOut(int id)
        {
            var visit = _repository.GetById(id);

            if (visit == null)
            {
                return NotFound();
            }

            return View(visit);
        }

        // POST: /MemberVisit/CheckOutConfirmed/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckOutConfirmed(int id)
        {
            var visit = _repository.GetById(id);

            if (visit == null)
            {
                return NotFound();
            }

            if (visit.Status == "Checked Out")
            {
                TempData["ErrorMessage"] =
                    "This member has already checked out.";

                return RedirectToAction(nameof(Index));
            }

            _repository.Checkout(id);

            TempData["SuccessMessage"] =
                "Member checkout successfully recorded.";

            return RedirectToAction(nameof(Index));
        }
    }
}