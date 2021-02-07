using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestWorking.Models
{
    public class PeopleContact
    {
        [Key, Column(Order = 0)]
        public int PeopleID { get; set; }
        [Key, Column(Order = 1)]
        public int ContactID { get; set; }

        public virtual People People { get; set; }
        public virtual Contact Contact { get; set; }

        [Key, Column(Order = 3)]
        public string Value { get; set; }
    }
}