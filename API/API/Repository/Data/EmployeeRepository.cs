using System;
using System.Collections;
using System.Collections.Generic;
using API.Context;
using API.EmployeeRepository;
using API.Model;
using API.Models;
using API.ViewModel;
using System.Linq;
using API.Hashing;

namespace API.Repository.Data
    {
        public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
        {
            private readonly MyContext context;
            public EmployeeRepository(MyContext myContext) : base(myContext)
            {
                context = myContext;
            }

            public int Register(RegisterVM registerVM)
            {
                Employee e = new Employee()
                {

                    NIK = registerVM.NIK,
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    Phone = registerVM.Phone,
                    BirthDate = registerVM.BirthDate,
                    Email = registerVM.Email,
                    Salary = registerVM.Salary

                };

                var cekNIK = context.Employees.Find(e.NIK);
                var cekEmail = context.Employees.Find(e.Email);
                var cekPhone = context.Employees.Find(e.Phone);

            if (cekNIK != null)
            {
                return 0;
            }
            else if (cekEmail != null)
            {
                return 0;
            }
            else if (cekPhone != null) {
                return 0;
            }


            context.Employees.Add(e);
                context.SaveChanges();
                Account a = new Account()
                {
                    NIK = registerVM.NIK,
                    Password = Hashing.Hashing.HassPassword(registerVM.Password)
                };
                context.Accounts.Add(a);
                context.SaveChanges();
                Education edu = new Education()
                {
                    Gpa = registerVM.Gpa,
                    Degree = registerVM.Degree,
                    UniversityId = registerVM.UniversityId
                };
                context.Education.Add(edu);
                context.SaveChanges();


                Profiling profiling = new Profiling();
                {
                    profiling.EducationId = edu.Id;
                    profiling.NIK = registerVM.NIK;
                }
                context.Profiling.Add(profiling);
                context.SaveChanges();
                AccountRole ar = new AccountRole();
                {
                
                ar.AccountNIK = registerVM.NIK;
                ar.RoleId = 1;
                }
                context.AccountRole.Add(ar);
                context.SaveChanges();

                var result = context.SaveChanges();
                return result;
                
            }

        public IEnumerable EmployeeAllData()
        {
            var query = from e in context.Set<Employee>()
                        join p in context.Set<Profiling>() on e.NIK equals p.NIK
                        join ed in context.Set<Education>() on p.EducationId equals ed.Id
                        join u in context.Set<University>() on ed.UniversityId equals u.Id

                        select new

                        {
                            e.FirstName,
                            e.LastName,
                            Gender = e.Gender == 0 ? "Male" : "Female",
                            e.Phone,
                            e.BirthDate,
                            e.Salary,
                            e.Email,
                            ed.Degree,
                            ed.Gpa,
                            u.Name
                        };
            return query.ToList();
        }


        //    public IEnumerable GetAllData()
        //{


        //    var query = from e in context.Set<Employee>()
        //                join p in context.Set<Profiling>() on e.NIK equals p.NIK
        //                join ed in context.Set<Education>() on p.EducationId equals ed.Id
        //                join u in context.Set<University>() on ed.UniversityId equals u.Id
        //                join a in context.Set < Account >()  on e.NIK equals a.NIK
        //                join ar in context.Set<AccountRole>() on a.NIK equals ar.AccountNIK
        //                join r in context.Set<Role>() on ar.RoleId equals r.Id


        //                select new
        //                {
        //                    e.FirstName,
        //                    e.LastName,
        //                    Gender = e.Gender == 0 ? "Male" : "Female",
        //                    e.Phone,
        //                    e.BirthDate,
        //                    e.Salary,
        //                    e.Email,
        //                    ed.Degree,
        //                    ed.Gpa,
        //                    u.Name,
        //                    RoleId = r.Id

        //                };
        //    return query.ToList();
        //}

        //public IEnumerable GetProfile(string Email)
        //{
        //    var query = from e in context.Set<Employee>()
        //                join p in context.Set<Profiling>() on e.NIK equals p.NIK
        //                join ac in context.Set<Account>() on e.NIK equals ac.NIK
        //                join ed in context.Set<Education>() on p.EducationId equals ed.Id
        //                join u in context.Set<University>() on ed.UniversityId equals u.Id
        //                where Email == e.Email 
        //                select new
        //                {
        //                    e.FirstName,
        //                    e.LastName,
        //                    Gender = e.Gender == 0 ? "Male" : "Female",
        //                    e.Phone,
        //                    e.BirthDate,
        //                    e.Salary,
        //                    e.Email,
        //                    ed.Degree,
        //                    ed.Gpa,
        //                    u.Name
        //                };
        //    return query.ToList();
        //}







    }

}

