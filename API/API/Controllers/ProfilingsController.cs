using System;
using System.Net;
using API.Base;
using API.Model;
using API.Models;
using API.Repository.Data;
using API.Repository.Interface;
using API.ViewModel;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilingsController : BasesController<Profiling, ProfilingRepository, string>
    {
        public ProfilingsController(ProfilingRepository repository) : base(repository)
        {
        }
    }
}
