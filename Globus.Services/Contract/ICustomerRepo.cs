using Globus.Data.Domain;
using Globus.Data.DTO;
using Globus.Utilities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globus.Services.Contract
{
    public interface ICustomerRepo<T> where T : class
    {
        Task<TransactionResponse<CustomerDTO>> Create(CustomerDTo customer);

        Task<bool> Update(long customerId, string Otp);

        Task<TransactionResponses<CustomerDTO>> GetCustomers();
    }
}
