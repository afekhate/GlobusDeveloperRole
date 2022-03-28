using Globus.Data.Domain;
using System;
using System.Collections.Generic;


namespace Globus.Data.Domain
{
    public class State : BaseObject
    {
        public string Name { get; set; }
        public List<LGA> lgas { get; set; }

     
    }
}
