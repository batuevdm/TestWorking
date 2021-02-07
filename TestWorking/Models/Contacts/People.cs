using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestWorking.Models
{
    public class People
    {
        [HiddenInput(DisplayValue = false)]
        public int PeopleID { get; set; }

        [MinLength(1), MaxLength(255)]
        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [MaxLength(255)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [MaxLength(255)]
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [Display(Name = "Дата рождения")]
        public DateTime? Birthday { get; set; }

        [MaxLength(255)]
        [Display(Name = "Организация")]
        public string Organization { get; set; }

        [MaxLength(255)]
        [Display(Name = "Должность")]
        public string Post { get; set; }

        public virtual ICollection<PeopleContact> PeopleContacts { get; set; }
    }
}