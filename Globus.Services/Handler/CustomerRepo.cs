using Globus.Data.Domain;
using Globus.Data.DTO;
using Globus.Services.Contract;
using Globus.Utilities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VatPay.Utilities.Common;

namespace Globus.Services.Handler
{
   
    public class CustomerRepo<T> : ICustomerRepo<T> where T : class
    {
        private readonly CustomerDbContext _context;

        public CustomerRepo()
        {
        }

        public CustomerRepo(CustomerDbContext context)
        {
            _context = context;
        }

        public async Task<TransactionResponse<CustomerDTO>> Create(CustomerDTo customer)
        {
            try
            {
                //Check if customer exist 
                var checkexist = await _context.Customers.AnyAsync(x => x.Email == customer.Email && x.Phone == customer.Phone);
                if (checkexist == true)
                {
                    var result = new TransactionResponse<CustomerDTO>
                    {
                        message = ApplicationResponseCode.LoadErrorMessageByCode("700").Name,
                        code = ApplicationResponseCode.LoadErrorMessageByCode("700").Code,
                        data = null
                    };
                    return result;
                }

                //Check if lga exist 
                var checklgaexist = await _context.LGAs.AnyAsync(x => x.Name == customer.LGA);
                if (checklgaexist == false)
                {
                    var result = new TransactionResponse<CustomerDTO>
                    {
                        message = ApplicationResponseCode.LoadErrorMessageByCode("703").Name,
                        code = ApplicationResponseCode.LoadErrorMessageByCode("703").Code,
                        data = null
                    };
                    return result;
                }

                //Check if lga exist in state
                var checklgaInState = await _context.LGAs.Where(x => x.Name == customer.LGA && x.StateId == (long)customer.State).FirstOrDefaultAsync();
                if (checklgaInState == null)
                {
                    var result = new TransactionResponse<CustomerDTO>
                    {
                        message = ApplicationResponseCode.LoadErrorMessageByCode("704").Name,
                        code = ApplicationResponseCode.LoadErrorMessageByCode("704").Code,
                        data = null
                    };
                    return result;
                }
                else
                {
                    var newCustomer = new Customer
                    {
                        Phone = customer.Phone,
                        Email = customer.Email,
                        Password = customer.Password,
                        StateId = (long)customer.State,
                        State = customer.State.ToString(),
                        LGA = customer.LGA,
                        LgaId = checklgaInState.ID,
                        CreatedDate = DateTime.Now,
                        CreatedBy = customer.Email,
                        OTP = "",
                        OtpVerified = false,
                        IsDeleted = false,
                        IsActive = false

                    };
                    await _context.Customers.AddAsync(newCustomer);
                    await _context.SaveChangesAsync();

                    CustomerDTO CustomerDTO = new CustomerDTO()
                    {
                        CustomerId = newCustomer.ID,
                        Phone = newCustomer.Phone,
                        Email = newCustomer.Email,
                        Password = newCustomer.Password,
                        State = newCustomer.State,
                        LGA = newCustomer.LGA,
                        CreatedDate = newCustomer.CreatedDate,
                        IsActive = false
                    };


                    var result = new TransactionResponse<CustomerDTO>
                    {
                        message = ApplicationResponseCode.LoadErrorMessageByCode("100").Name,
                        code = ApplicationResponseCode.LoadErrorMessageByCode("100").Code,
                        data = CustomerDTO

                    };
                    return result;

                }
            }
            catch (Exception ex)
            {

                var result = new TransactionResponse<CustomerDTO>
                {
                    message = ApplicationResponseCode.LoadErrorMessageByCode("808").Name,
                    code = ApplicationResponseCode.LoadErrorMessageByCode("808").Code,
                    data = null

                };
                return result;
            }

            
          
        }

        public async Task<TransactionResponses<CustomerDTO>> GetCustomers()
        {
            try
            {
                var customers = await _context.Customers.ToListAsync();
                if (customers.Count == 0)
                {
                    var result = new TransactionResponses<CustomerDTO>
                    {
                        message = ApplicationResponseCode.LoadErrorMessageByCode("115").Name,
                        code = ApplicationResponseCode.LoadErrorMessageByCode("115").Code,
                        data = null

                    };
                    return result;
                }
                else
                {
                    var cust = customers.Select(
                     x => new CustomerDTO
                     {
                         CustomerId = x.ID,
                         Phone = x.Phone,
                         Email = x.Email,
                         Password = x.Password,
                         State = x.State,
                         LGA = x.LGA,
                         OTP = x.OTP,
                         OtpVerified = x.OtpVerified,
                         CreatedBy = x.Email,
                         CreatedDate = x.CreatedDate,
                         IsActive = x.IsActive
                     }).ToList();

                    var result = new TransactionResponses<CustomerDTO>
                    {
                        message = ApplicationResponseCode.LoadErrorMessageByCode("100").Name,
                        code = ApplicationResponseCode.LoadErrorMessageByCode("100").Code,
                        data = cust

                    };
                    return result;
                }
                    
               
             }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(long customerId, string otp)
        {
            try
            {
                var checkexist = await _context.Customers.Where(x => x.ID == customerId).FirstOrDefaultAsync();
                
                checkexist.OTP = otp;
                checkexist.OtpVerified = false;
                await _context.SaveChangesAsync();
                return true;
              
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
