using System;
using API.Context;
using API.EmployeeRepository;
using API.Models;
using API.ViewModel;

namespace API.Repository.Data
{
    public class AccountRoleRepository : GeneralRepository<MyContext, AccountRole, int>
    {
        private readonly MyContext context;
        public AccountRoleRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }



        public int SignManager(AccountRole role)
        {

            AccountRole a = new AccountRole()
            {

                RoleId = role.RoleId,
                AccountNIK = role.AccountNIK

            };
            context.AccountRole.Add(a);
            var result = context.SaveChanges();
            return result;

        }
    }
}
