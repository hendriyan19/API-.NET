//using System;
//using System.Collections.Generic;
//using System.Linq;
//using API.Context;
//using API.Models;
//using Microsoft.EntityFrameworkCore;

//namespace API.Repository
//{
//    public class EmployeeRepository : Interface.EmployeeRepository
//    {
//        private readonly MyContext context;

//        public EmployeeRepository(MyContext context)
//        {
//            this.context = context;
//        }

//        public int Delete(string NIK)
//        {
//            var entity = context.Employees.Find(NIK);
//            context.Remove(entity);
//            var result = context.SaveChanges();
//            return result;
//        }

//        public IEnumerable<Employee> get()
//        {
//            return context.Employees.ToList();
//        }

//        public Employee Get(string NIK)
//        {
//            return context.Employees.FirstOrDefault(x => x.NIK == NIK);
//        }

//        public IEnumerable<Employee> Get()
//        {
//            throw new NotImplementedException();
//        }

//        public int Insert(Employee employee)
//        {
//            var check = context.Employees.Find(employee.NIK);

//            if (check == null)
//            {
//                context.Employees.Add(employee);
//            }

//            var result = context.SaveChanges();
//            return result;
//        }

//        public int Update(Employee employee)
//        {
//            var UpdateData = context.Entry(employee).State = EntityState.Modified;

//            if (UpdateData != 0)
//            {
//                context.Employees.Update(employee);
//            }
//            else if (UpdateData == 0)
//            {

//            }
//            var result = context.SaveChanges();
//            return result;
//        }
//    }


//}