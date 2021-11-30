using System;
using System.Collections.Generic;
using System.Net;
using API.Base;
using API.Models;
using API.Repository.Data;
using API.Repository.Interface;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BasesController<Employee, Repository.Data.EmployeeRepository, string>
    {

        private Repository.Data.EmployeeRepository employeeRepository;
        public IConfiguration _configuration;
        public EmployeesController(Repository.Data.EmployeeRepository repository, IConfiguration configuration) : base(repository)
        {
            this.employeeRepository = repository;
            this._configuration = configuration;
        }

        [HttpPost("Register")]
        public ActionResult Post(RegisterVM registerVM)
        {
            try
            {
                var result = employeeRepository.Register(registerVM);

                if (result == 0)
                {

                    
                    return Ok(new { status = HttpStatusCode.OK, result, Message = "Data berhasil di buat" });
                }

                else
                {

                    return BadRequest(new { Status = HttpStatusCode.BadRequest, Message = "Gagal" });
                }
            }
            catch (Exception e)

            {
                return BadRequest(new { Status = HttpStatusCode.BadRequest, Message = e });
            }

        }

        [HttpGet("TestCORS")]
        public ActionResult TestCORS()
        {
            return Ok("Test CORS Berhasil");
        }

        //[HttpGet("Profil/{Email}")]
        //public ActionResult Profil(string Email)
        //{
        //    var result = employeeRepository.GetProfile(Email);
        //    return Ok(new { status = HttpStatusCode.OK, result, Message = "Data Muncul" });
        //}
        //[Authorize(Roles = "Director,Manager")]
        //[HttpGet("GetAll")]
        //public ActionResult GetAll()
        //{
        //    var result = employeeRepository.GetAllData();
        //    return Ok(new { status = HttpStatusCode.OK, result, Message = "Data berhasil diambil" });
        //}

        [Authorize(Roles ="Director,Manager")]
        [HttpGet("Get")]
        public ActionResult GetAll()
        {

            var result = employeeRepository.EmployeeAllData();
            return Ok(new { status = HttpStatusCode.OK, result, Message = "Data berhasil di tembak" });
        }



    }
}
