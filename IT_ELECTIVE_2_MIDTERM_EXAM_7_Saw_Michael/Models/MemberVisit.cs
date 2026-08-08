using System.ComponentModel.DataAnnotations;

namespace IT_ELECTIVE_2_MIDTERM_EXAM_7_Saw_Michael.Models
{
    public class MemberVisit
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Visit Number")]
        public string VisitNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        [Display(Name = "Member ID")]
        public string MemberId { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Display(Name = "Membership Type")]
        public string MembershipType { get; set; } = string.Empty;

        [Required]
        [Phone]
        [StringLength(20)]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Visit Date")]
        public DateTime VisitDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Time In")]
        public DateTime TimeIn { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Time Out")]
        public DateTime? TimeOut { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Inside Gym";

        [Required]
        [StringLength(100)]
        [Display(Name = "Workout Purpose")]
        public string WorkoutPurpose { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Notes { get; set; }
    }
}