using System;
namespace API.ViewModel
{
    public class ChangePass
    {
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Degree { get; set; }
        public string Gpa { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string OldPassword { get; set; }
        public int UniversityId { get; set; }
    }
}