using Globus.Data.DTO;
using Globus.Services.Contract;
using Globus.Utilities.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VatPay.Utilities.Common;

namespace Globus.Api.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GoldPriceController : ControllerBase
    {
        public GoldPriceController()
        {

        }

        [HttpGet]
        [Route("GetCurrentPrice")]
        [ProducesResponseType(typeof(ApiResult<MetalPriceDTO>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetCurrentPrice()
        {

            try
            {
                var result = new ApiResult<MetalPriceDTO>();

                var responseMessage = await MiddleWare.PostRapidAsync();
                var deserializePrice = JsonConvert.DeserializeObject<MetalPriceDTO>(responseMessage.Content);



                if (responseMessage.Content != null || responseMessage.Content != "")
                {
                    result = new ApiResult<MetalPriceDTO>
                    {
                        HasError = false,
                        Result = deserializePrice,
                        Message = ApplicationResponseCode.LoadErrorMessageByCode("100").Name,
                        StatusCode = ApplicationResponseCode.LoadErrorMessageByCode("100").Code,
                       
                    };
                    return Ok(result);


                }
                else
                {
                    result = new ApiResult<MetalPriceDTO>
                    {
                        HasError = true,
                        Result = null,
                        Message = ApplicationResponseCode.LoadErrorMessageByCode("100").Name,
                        StatusCode = ApplicationResponseCode.LoadErrorMessageByCode("100").Code,

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


