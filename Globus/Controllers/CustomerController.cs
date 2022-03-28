using Globus.Data.DTO;
using Globus.Services.Contract;
using Globus.Utilities.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VatPay.Utilities.Common;

namespace Globus.Api.Controllers
{
    
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private ICustomerRepo<CustomerDTo> _customerRepo;
        private IOtpRepo _otpRepo;
        public CustomerController(ICustomerRepo<CustomerDTo> customerRepo, IOtpRepo otpRepo)
        {
            _customerRepo = customerRepo;
            _otpRepo = otpRepo;

        }



        [HttpPost]
        [Route("Onboard")]
        [ProducesResponseType(typeof(ApiResult<CustomerDTo>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Onboard([FromForm]CustomerDTo payload)
        {
            
            try
            {
                var result = new ApiResult<CustomerDTO>();

                var responseMessage = await _customerRepo.Create(payload);

                if (responseMessage.data != null && responseMessage.code == "100")
                {
                    result = new ApiResult<CustomerDTO>
                    {
                        HasError = false,
                        Result = responseMessage.data,
                        Message = responseMessage.message,
                        StatusCode = responseMessage.code
                    };


                    //sending Otp
                    var otpResponse = await _otpRepo.SendOtp(result.Result.Email);
                    if(otpResponse != null)
                    {
                        await _customerRepo.Update(result.Result.CustomerId, otpResponse);
                    }
                    return Ok(result); 
                }
                else
                {
                    result = new ApiResult<CustomerDTO>
                    {
                        HasError = true,
                        Result = responseMessage.data,
                        Message = responseMessage.message,
                        StatusCode = responseMessage.code
                    };
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {

                var u = new ApiResult<CustomerDTO>
                {
                    HasError = true,
                    Result = null,
                    Message = ex.Message,
                    StatusCode = ApplicationResponseCode.LoadErrorMessageByCode("1000").Code
                };
                return BadRequest(u);
            }
        }


        [HttpGet]
        [Route("Customers")]
        [ProducesResponseType(typeof(ApiResult<CustomerDTO>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Customers()
        {

            try
            {
                var result = new ApiResult<List<CustomerDTO>>();

                var responseMessage = await _customerRepo.GetCustomers();

                if (responseMessage.data != null && responseMessage.code == "100")
                {
                    result = new ApiResult<List<CustomerDTO>>
                    {
                        HasError = false,
                        Result = responseMessage.data,
                        Message = responseMessage.message,
                        StatusCode = responseMessage.code
                    };

                    return Ok(result);


                }
                else
                {
                    result = new ApiResult<List<CustomerDTO>>
                    {
                        HasError = true,
                        Result = responseMessage.data,
                        Message = responseMessage.message,
                        StatusCode = responseMessage.code
                    };
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {

                var u = new ApiResult<CustomerDTO>
                {
                    HasError = true,
                    Result = null,
                    Message = ex.Message,
                    StatusCode = ApplicationResponseCode.LoadErrorMessageByCode("1000").Code
                };
                return BadRequest(u);
            }
        }
    }

}


