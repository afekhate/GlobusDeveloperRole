using Globus.Data.DTO;
using Globus.Services.Contract;
using Globus.Utilities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globus.Services.Handler
{
    public class OtpRepo : IOtpRepo
    {
        public async Task<string> SendOtp(string Email)
        {
            var otp = GenericUtil.GenerateOTP();
            SendingOtp(otp, Email);
            return  otp;
        }

        //Mocking Otp sending functionality
        public void SendingOtp(string otp, string Email)
        {
            Console.WriteLine("Otp sent to customer");
            
        }
    }
}
