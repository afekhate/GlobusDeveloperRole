using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globus.Data.Domain
{
    public class Customer : BaseObject
    {
        
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string State { get; set; }
        public string LGA { get; set; }

        public long StateId { get; set; }

        public long LgaId { get; set; }
        public string OTP { get; set; }
        public bool OtpVerified { get; set; }

    }
}
