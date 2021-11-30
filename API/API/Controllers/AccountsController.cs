using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using API.Base;
using API.Context;
using API.Model;
using API.Models;
using API.Repository.Data;
using API.Repository.Interface;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BasesController<Account, AccountRepository, string>
    {

        private Repository.Data.AccountRepository accountRepository;
        public IConfiguration configuration;
        private readonly MyContext context;
        public AccountsController(AccountRepository repository, IConfiguration configuration, MyContext context) : base(repository)
        {
            this.accountRepository = repository;
            this.configuration = configuration;
            this.context = context;

            
        }


        [Authorize]
        [HttpPost("TestJwt")]
        public ActionResult testJwt() {
            return Ok("JWT BERHASIL");
        }

        [Authorize]
        [HttpPost("LoginToken")]
        public ActionResult PostLoginToken(LoginVM loginVM) {
            var getEmail = context.Employees.Where(e => e.Email == loginVM.Email).FirstOrDefault();
            var GetUserData = (from e in context.Set<Employee>()
                               join p in context.Set<Profiling>() on e.NIK equals p.NIK
                               join ed in context.Set<Education>() on p.EducationId equals ed.Id
                               join u in context.Set<University>() on ed.UniversityId equals u.Id
                               join a in context.Set<Account>() on e.NIK equals a.NIK
                               join ar in context.Set<AccountRole>() on a.NIK equals ar.AccountNIK
                               join r in context.Set<Role>() on ar.RoleId equals r.Id
                               where e.Email == loginVM.Email
                               select r.Name).Single();
            var data = new LoginVM
            {
                Email = GetUserData,
                RoleName = GetUserData
            };
            var claims = new List<Claim>
            {
                new Claim("Email",data.Email),
                new Claim("Role",data.RoleName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["Jwt : Issuer"],
                configuration["Jwt : Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn
                );
            var idtoken = new JwtSecurityTokenHandler().WriteToken(token);
            claims.Add(new Claim("TokenSecurity", idtoken.ToString()));
            return Ok(new { status = HttpStatusCode.OK, Token = idtoken, message = "Login Sucess!" });

        }

        [HttpPost("Login")]
        public ActionResult Post(LoginVM loginVM)
        {
            var result = accountRepository.Login(loginVM);
            if (result == 1)
            {

                //var getUserData = (from e in context.Employees
                //                   where e.Email == loginVM.Email
                //                   join a in context.Set<Account>() on e.NIK equals a.NIK
                //                   join ar in context.Set<AccountRole>() on e.NIK equals ar.AccountNIK
                //                   join r in context.Set<Role>() on ar.RoleId equals r.Id
                //                   select new
                //                   {
                //                       Name = r.Name,
                //                       Email = e.Email
                //                   }).ToList();


                //var claims = new List<Claim>
                //    {
                //        new Claim("Email", getUserData[0].Email),
                //    };
                //foreach (var getRole in getUserData)
                //{
                //    claims.Add(new Claim(ClaimTypes.Role, getRole.Name));
                //}
                //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]));
                //var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                //var token = new JwtSecurityToken(
                //    configuration["Jwt:Issuer"],
                //    configuration["Jwt:Audience"],
                //    claims,
                //    expires: DateTime.UtcNow.AddMinutes(10),
                //    signingCredentials: signIn
                //    );

                //var idtoken = new JwtSecurityTokenHandler().WriteToken(token);
                //claims.Add(new Claim("TokenSecurity", idtoken.ToString()));
                //return Ok(new { status = HttpStatusCode.OK, idtoken, message = "Login Succes"});

                var getEmail = context.Employees.Where(e => e.Email == loginVM.Email).SingleOrDefault();
                var getRole = context.Role.Where(r => r.AccountRoles.Any(ar => ar.Account.NIK == getEmail.NIK)).ToList();
                var subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, getEmail.Email)
                });
                foreach (var item in getRole)
                {
                    subject.AddClaim(new Claim(ClaimTypes.Role, item.Name));
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = subject,
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials
                    (
                            new SymmetricSecurityKey(tokenKey),
                            SecurityAlgorithms.HmacSha256Signature
                    )
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var idtoken = tokenHandler.WriteToken(token);
                return Ok(new { status = HttpStatusCode.OK, idtoken, message = "Login Success!" });







            }
            else if (result == 2)
            {
                return NotFound(new { status = HttpStatusCode.NoContent, result, messageResult = "Password Salah" });
            }
            else if (result == 3)
            {
                return NotFound(new { status = HttpStatusCode.NoContent, result, messageResult = "Email atau Phone salah" });
            }
            return NotFound(new { status = HttpStatusCode.NoContent, result, messageResult = "gagal login" });
        }

        [HttpPost("ForgetUserPassword")]
        public ActionResult ForgotPassword(ForgotPasswordVM forgotPasswordVM) {
            var result = accountRepository.ForgetUserPassword(forgotPasswordVM);

            if (result == 1) {
                return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = "Password Baru Terkirim Ke Email Anda" });
            }

            return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = "Data tidak ditemukan" });
        }


        [HttpPost("changepass")]
        public ActionResult ChangePassword(ChangePass changePass)
        {
            var result = accountRepository.ChangePassword(changePass);
            if (result == 2)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Email tidak ditemukan" });
            }
            else if (result == 3)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = "Konfirmasi password tidak sesuai dengan new password" });
            }
            else if (result == 1)
            {
                return Ok(new { status = HttpStatusCode.OK, result = result, message = "Data Password kamu telah diperbarui" });
            }
            else if (result == 4)
            {
                return Ok(new { status = HttpStatusCode.NotFound, result = result, message = "Password lama tidak sesuai dengan database" });
            }
            return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Data tidak ditemukan" });
        }





    }
}
