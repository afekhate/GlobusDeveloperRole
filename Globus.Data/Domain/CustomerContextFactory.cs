using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globus.Data.Domain
{
    public class CustomerContextFactory : IDesignTimeDbContextFactory<CustomerDbContext>
    {
        public CustomerDbContext CreateDbContext(string[] args)
        {
            var optionsBUilder = new DbContextOptionsBuilder<CustomerDbContext>();
            optionsBUilder.UseSqlServer("Server=localhost;Database=GlobCustomerDb;persist security info=true;user id=sa;password=password;MultipleActiveResultSets=true");

            return new CustomerDbContext(optionsBUilder.Options);
        }
    }


    


}
