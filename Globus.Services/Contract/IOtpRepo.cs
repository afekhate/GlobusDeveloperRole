using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globus.Services.Contract
{
    public interface IOtpRepo
    {
        Task<string> SendOtp(string Email);

    }
}
