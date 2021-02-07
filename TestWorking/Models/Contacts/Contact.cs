using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestWorking.Models
{
    public class Contact
    {
        public int ContactID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Type { get; set; }

        public virtual ICollection<PeopleContact> PeopleContacts { get; set; }
    }
}