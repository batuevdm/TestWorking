using System;
using System.Data.Entity;
using System.Linq;

namespace TestWorking.Models
{
    public class ContactsContext : DbContext
    {
        public ContactsContext()
            : base("name=ContactsContext")
        {
        }

        public DbSet<People> Peoples { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<PeopleContact> PeopleContacts { get; set; }
    }
}