//using API.Context;
//using API.Models;
//using API.Repository;
//using API.Repository.Interface;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Threading.Tasks;

//namespace API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]

//    public class EmployeeController : ControllerBase
//    {
//        private EmployeeRepository employeeRepository;
//        private string messageResult;
//        private int statusCode;
//        public EmployeeController(EmployeeRepository employeeRepository)
//        {
//            this.employeeRepository = employeeRepository;
//        }


//        [HttpPost]
//        public ActionResult Post(Employee employee)
//        {
//            var check = employeeRepository.Insert(employee);


//            if (check != 0)
//            {
//                return Ok(new
//                {
//                    status = HttpStatusCode.OK,
//                    data = employeeRepository.Insert(employee),
//                    message = "Data Berhasil Di Insert"
//                });

//            }
//            return NotFound(new
//            {
//                status = HttpStatusCode.NotFound,
//                data = "",
//                message = "Data Tidak bisa di Insert"
//            });


//        }



//        [HttpPut]
//        public ActionResult Update(Employee employee)
//        {

//            var UpdateData = employeeRepository.Update(employee);

//            if (UpdateData != 0)
//            {
//                return Ok(new
//                {
//                    status = HttpStatusCode.OK,
//                    data = employeeRepository.Update(employee),
//                    message = "Data Berhasil Di Update"
//                });

//            }
//            return NotFound(new
//            {
//                status = HttpStatusCode.NotFound,
//                data = "",
//                message = "Data Tidak bisa di Update"
//            });


//        }

//        [HttpDelete("{NIK}")]
//        public ActionResult Delete(string NIK)
//        {
//            employeeRepository.Delete(NIK);
//            return Ok();
//        }

//        [HttpGet("{NIK}")]
//        public ActionResult Get(string NIK)
//        {
//            switch (Response.StatusCode)
//            {
//                case StatusCodes.Status200OK:
//                    statusCode = 200;
//                    messageResult = "Data Berhasil Ditemukan";
//                    break;
//                case StatusCodes.Status404NotFound:
//                    statusCode = 404;
//                    messageResult = "Data tidak ditemukan";
//                    break;
//                case StatusCodes.Status204NoContent:
//                    statusCode = 204;
//                    messageResult = "Tidak ada Data diterima";
//                    break;
//                default:
//                    statusCode = 0;
//                    messageResult = "Result tidak Diterima";
//                    break;
//            };
//            var result = new
//            {
//                status = statusCode,
//                data = employeeRepository.Get(NIK),
//                message = messageResult
//            };
//            return Ok(JsonConvert.SerializeObject(result));
//        }

        



//    }
//}