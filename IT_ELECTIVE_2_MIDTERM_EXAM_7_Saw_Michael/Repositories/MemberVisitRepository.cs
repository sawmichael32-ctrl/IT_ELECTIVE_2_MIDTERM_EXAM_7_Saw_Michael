using IT_ELECTIVE_2_MIDTERM_EXAM_7_Saw_Michael.Models;

namespace IT_ELECTIVE_2_MIDTERM_EXAM_7_Saw_Michael.Repositories
{
    public class MemberVisitRepository
    {
        private static readonly List<MemberVisit> visits = new List<MemberVisit>
        {
            new MemberVisit
            {
                Id = 1,
                VisitNumber = "VIS-001",
                MemberId = "MEM-001",
                FirstName = "Juan",
                LastName = "Dela Cruz",
                MembershipType = "Premium",
                ContactNumber = "09171234567",
                VisitDate = DateTime.Today,
                TimeIn = DateTime.Today.AddHours(8),
                TimeOut = null,
                Status = "Inside Gym",
                WorkoutPurpose = "Strength Training",
                Notes = "Regular workout"
            },

            new MemberVisit
            {
                Id = 2,
                VisitNumber = "VIS-002",
                MemberId = "MEM-002",
                FirstName = "Maria",
                LastName = "Santos",
                MembershipType = "Regular",
                ContactNumber = "09181234567",
                VisitDate = DateTime.Today,
                TimeIn = DateTime.Today.AddHours(9),
                TimeOut = DateTime.Today.AddHours(10),
                Status = "Checked Out",
                WorkoutPurpose = "Cardio",
                Notes = "Completed workout"
            }
        };

        public List<MemberVisit> GetAll()
        {
            return visits;
        }

        public MemberVisit? GetById(int id)
        {
            return visits.FirstOrDefault(v => v.Id == id);
        }

        public void Add(MemberVisit visit)
        {
            visit.Id = visits.Count == 0
                ? 1
                : visits.Max(v => v.Id) + 1;

            if (string.IsNullOrWhiteSpace(visit.VisitNumber))
            {
                visit.VisitNumber = $"VIS-{visit.Id:D3}";
            }

            visit.Status = "Inside Gym";
            visit.TimeOut = null;

            visits.Add(visit);
        }

        public void Update(MemberVisit visit)
        {
            var existing = GetById(visit.Id);

            if (existing == null)
            {
                return;
            }

            existing.VisitNumber = visit.VisitNumber;
            existing.MemberId = visit.MemberId;
            existing.FirstName = visit.FirstName;
            existing.LastName = visit.LastName;
            existing.MembershipType = visit.MembershipType;
            existing.ContactNumber = visit.ContactNumber;
            existing.VisitDate = visit.VisitDate;
            existing.TimeIn = visit.TimeIn;
            existing.TimeOut = visit.TimeOut;
            existing.Status = visit.Status;
            existing.WorkoutPurpose = visit.WorkoutPurpose;
            existing.Notes = visit.Notes;
        }

        public void Checkout(int id)
        {
            var visit = GetById(id);

            if (visit == null)
            {
                return;
            }

            visit.TimeOut = DateTime.Now;
            visit.Status = "Checked Out";
        }
    }
}