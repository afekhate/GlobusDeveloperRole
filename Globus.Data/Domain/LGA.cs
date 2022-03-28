using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace Globus.Data.Domain
{
    public class LGA:BaseObject
    {
        public string Name { get; set; }

        public long StateId { get; set; }
       
        public State State { get; set; }

    }
}
