using System;
using API.Context;
using API.EmployeeRepository;
using API.Model;

namespace API.Repository.Data
{
    public class EducationRepository : GeneralRepository<MyContext, Education, string>
    {
       
        public EducationRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
