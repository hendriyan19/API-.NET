using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace.API.Controllers
{
    [Route("api/controller")]
    [ApiController]

    public class EmployeeController : ControllerBase
    {

        private EmployeeRepository employeeRepository;
        private readonly MyContext context;

        public EmployeeController(MyContext context)
        {

            this.context = context;
            employeeRepository = new EmployeeRepository(context);
        }
        [HttpPost]

        public ActionResult Post(Employee employee)
        {
            return Ok();
        }


    }
    }