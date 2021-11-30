using System;
using System.Net;
using API.Base;
using API.Context;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class AccountRoleController : BasesController<AccountRole, AccountRoleRepository, int>
    {
        private readonly AccountRoleRepository accountRoleRepository;
        private readonly MyContext context;
        public AccountRoleController(AccountRoleRepository repository, MyContext myContext) : base(repository)
        {
            this.accountRoleRepository = repository;
            this.context = myContext;

        }


        //[Authorize(Roles = "Director,Manager")]
        //[HttpPost("SignManager")]
        //public ActionResult PostManager(AccountRole accountRole)
        //{
        //    try
        //    {
        //        var result = accountRoleRepository.SignManager(accountRole);

        //        if (result != 1)
        //        {
        //            return BadRequest(new { Status = HttpStatusCode.BadRequest, Message = "data sama" });
        //        }
        //        else
        //        {
        //            return Ok(new { status = HttpStatusCode.OK, result, Message = "Data berhasil di create" });
        //        }
        //    }
        //    catch (Exception)

        //    {
        //        return BadRequest(new { Status = HttpStatusCode.BadRequest, Message = "Gagal Masuk Data" });
        //    }

        //}


        [Authorize(Roles ="Director")]
        [HttpPost("SignManager")]
        public ActionResult PostManager(AccountRole role)
        {
            try
            {
                var result = accountRoleRepository.SignManager(role);

                return Ok(new { status = HttpStatusCode.OK, result, Message = "Data berhasil di buat" });
               
            }
            catch (Exception)

            {
                return BadRequest(new { Status = HttpStatusCode.BadRequest, Message = "Gagal Masuk Data" });
            }

        }
    }
}