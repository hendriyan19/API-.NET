using System;
using API.Context;
using API.EmployeeRepository;
using API.Model;

namespace API.Repository.Data
{
    public class ProfilingRepository : GeneralRepository<MyContext, Profiling, string>
    {
        public ProfilingRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
