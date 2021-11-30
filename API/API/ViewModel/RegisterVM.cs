using System;
using Microsoft.EntityFrameworkCore;
namespace API.ViewModel
{
    public class RegisterVM
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
        public int UniversityId { get; set; }
        public int RoleId { get; set; }


    }

}

