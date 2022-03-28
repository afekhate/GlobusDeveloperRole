using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globus.Data.DTO
{
   public  class Location
    {
        public class StateLGA
        {
            public string state { get; set; }
            public List<string> lgas { get; set; }
        }

        public class RootObject
        {
            public List<StateLGA> states { get; set; }

        }
    }
}
