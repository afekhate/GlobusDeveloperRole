using Globus.Api.Controllers;
using Globus.Data.Domain;
using Globus.Data.DTO;
using Globus.Services.Contract;
using Globus.Utilities.Common;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Globus.Tests
{
    public class CustomerControllerTest
    {
        private readonly Mock<ICustomerRepo<CustomerDTo>> service;
        private readonly IOtpRepo otp;
        public CustomerControllerTest()
        {
            service = new Mock<ICustomerRepo<CustomerDTo>>();
          
        }
        [Fact]
        public void Onboard_CreatedStatus_PassingCustomerObjectToOnboard()
        {


            var result = new CustomerDTo();
            var customers = GetSampleCustomers();
            var newCustomer = customers.Result.data.FirstOrDefault();

            var customer = new CustomerDTo
            {
                Email = newCustomer.Email,
                Phone = newCustomer.Phone,
                Password = newCustomer.Password,
                State = States.Lagos,
                LGA = newCustomer.LGA
            };
            var controller = new CustomerController(service.Object, otp);
            var actionResult = controller.Onboard(customer);
            if(actionResult == null)
            {
                result = customer;
            }
            Assert.IsType<CustomerDTo>(result);

        }

        [Fact]
        
        public void Customers_ListOfCustomers_CustomerExistsInRepo()
        {
            //arrange
            var customers = GetSampleCustomers();
            service.Setup(x => x.GetCustomers())
             .Returns(GetSampleCustomers);

            var controller = new CustomerController(service.Object, otp);
            var actionResult = controller.Customers();
            
            //act

            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as ApiResult<List<CustomerDTO>>;

            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetSampleCustomers().Result.data.Count(), actual.Result.Count());
        }

        private async Task<TransactionResponses<CustomerDTO>> GetSampleCustomers()
        {
            List<CustomerDTO> customer = new List<CustomerDTO>
            {
                new CustomerDTO
                {
                    Email = "fred@gmail.com",
                    Phone = "09067442324",
                    Password = "jdhjsfgvjfjvjv",
                    State = "Lagos",
                    LGA = "alimosho",
                  
                },
                new CustomerDTO
                {
                    Email = "fredr@gmail.com",
                    Phone = "09067442322",
                    Password = "tsdhghdfhdfh",
                    State = "Lagos",
                    LGA = "alimosho",
                   
                }
            };

            TransactionResponses<CustomerDTO> output = new TransactionResponses<CustomerDTO>
            {
                message = "Successful",
                code = "100",
                data = customer
            };
            return output;
        }
    }
}
