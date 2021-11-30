using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using API.Context;
using API.EmployeeRepository;
using API.Model;
using API.Models;
using API.ViewModel;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext context;

        public AccountRepository(MyContext myContext) : base(myContext)
        {
            context = myContext;
        }

        public int assignManager(AccountRole accountRole)
        {
            AccountRole ar = new AccountRole();
            {
                ar.AccountNIK = accountRole.AccountNIK;
                ar.RoleId = 2;
            }
            context.AccountRole.Add(ar);
            context.SaveChanges();
            var result = context.SaveChanges();
            return result;
        }


        public int Login(LoginVM loginVM)
        {
            var result = 0;
            try
            {
                var getEmail = context.Employees.Where(e => e.Email == loginVM.Email).FirstOrDefault();
                var getPhone = context.Employees.Where(e => e.Phone == loginVM.Phone).FirstOrDefault();

                var pass = (from e in context.Set<Employee>()
                            join a in context.Set<Account>() on e.NIK equals a.NIK
                            where e.Email == loginVM.Email || e.Phone == loginVM.Phone
                            select a.Password).Single();

                if (getEmail != null || getPhone != null)
                {
                    var getPassword = Hashing.Hashing.ValidatePassword(loginVM.Password, pass);
                    if (getPassword)
                    {
                        result = 1;
                    }
                    else
                    {
                        result = 2;
                    }
                }
                else
                {
                    result = 3;
                }

            }
            catch (Exception)
            {
                result = 0;
            }

            return result;
        }

        

        public int ForgetUserPassword(ForgotPasswordVM forgotPasswordVM)
        {
            var checkEmail = context.Employees.Where(Em => Em.Email == forgotPasswordVM.Email).FirstOrDefault();
            if (checkEmail != null)
            {
                

                string NewPassword = Guid.NewGuid().ToString().Substring(0, 12);
                var NIK = (from e in context.Set<Employee>()
                           where e.Email == forgotPasswordVM.Email
                           join a in context.Set<Account>() on e.NIK equals a.NIK
                           select e.NIK).Single();


                var original = context.Accounts.Find(NIK);
                DateTimeOffset now = (DateTimeOffset)DateTime.Now;

                if (original != null)
                {
                    original.Password = Hashing.Hashing.HassPassword(NewPassword);
                    context.SaveChanges();
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.To.Add(forgotPasswordVM.Email);
                    mailMessage.From = new MailAddress("vainngod@gmail.com", "Your New Password", System.Text.Encoding.UTF8);
                    mailMessage.Subject = "Forgot Password API System " +now;
                    mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                    mailMessage.Body = "Hi There! Your New Password is " + NewPassword;
                    mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Priority = MailPriority.High;
                    SmtpClient client = new SmtpClient();
                    client.Credentials = new System.Net.NetworkCredential("vainngod@gmail.com", "Vevevovivo123");
                    client.Port = 587;
                    client.Host = "smtp.gmail.com";
                    client.EnableSsl = true;
                    try
                    {
                        client.Send(mailMessage);
                        return 1;
                    }
                    catch (Exception)
                    {
                        return 3;
                    }
                }
            }
            return 2;
        }

      



        public IEnumerable GetProfile(string Email)
        {
            var query = from e in context.Set<Employee>()
                        join p in context.Set<Profiling>() on e.NIK equals p.NIK
                        join ac in context.Set<Account>() on e.NIK equals ac.NIK
                        join ed in context.Set<Education>() on p.EducationId equals ed.Id
                        join u in context.Set<University>() on ed.UniversityId equals u.Id
                        where Email == e.Email 
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
        
        public int ChangePassword(ChangePass changePass)
        {
            

            var checkEmail = context.Employees.Where(b => b.Email == changePass.Email).FirstOrDefault();
           
            if (checkEmail != null)
            {
                if (changePass.NewPassword == changePass.ConfirmPassword)
                {
                    var nik = (from e in context.Set<Employee>()
                               where e.Email == changePass.Email
                               join a in context.Set<Account>() on e.NIK equals a.NIK
                               select e.NIK).Single();
                    var password = (from e in context.Set<Employee>()
                                    where e.Email == changePass.Email
                                    join a in context.Set<Account>() on e.NIK equals a.NIK
                                    select a.Password).Single();
                    
                    var checkPassword = Hashing.Hashing.ValidatePassword(changePass.OldPassword, password);
                    if (checkPassword == false)
                    {
                        return 4;
                    }
                    var original = context.Accounts.Find(nik);
                    if (original != null)
                    {
                       
                        original.Password = Hashing.Hashing.HassPassword(changePass.NewPassword);
                        context.SaveChanges();
                        return 1;
                    }
                }
                else
                {
                    return 3;
                }
            }
            return 2;
        }



    }
}
