using System;
using System.Collections;
using System.Linq;
using API.Context;
using API.EmployeeRepository;
using API.Model;
using API.Models;

namespace API.Repository.Data
{
    public class UniversityRepository : GeneralRepository<MyContext, University, int>
    {
        private readonly MyContext context;
        public UniversityRepository(MyContext myContext) : base(myContext)
        {
            context = myContext;
        }

        public IEnumerable CountByUniversity() {
            var GetUniversity = (from p in context.Profiling
                            join ed in context.Education on p.EducationId equals ed.Id
                            join u in context.University on ed.UniversityId equals u.Id
                            group u by new { ed.UniversityId, u.Name } into c
                            select new {
                               Value = c.Count(), 
                                Universitas = c.Key.Name,
                            });
            return GetUniversity;

        }
    }
}
