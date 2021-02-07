using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using TestWorking.Models;

namespace TestWorking.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ContactsContext context;

        private SelectList GetContactTypes()
        {
            return new SelectList(context.Contacts.ToList(), "ContactID", "Type");
        }
        public ContactsController()
        {
            context = new ContactsContext();
        }
        public ActionResult Index(string search)
        {
            IOrderedQueryable<PeopleContact> contacts;
            if (string.IsNullOrEmpty(search))
            {
                // Все контакты
                contacts = context.PeopleContacts
                    .OrderBy(p => p.People.LastName)
                    .ThenBy(p => p.People.FirstName)
                    .ThenBy(p => p.People.MiddleName);
            }
            else
            {
                // Поиск по полям
                contacts = context.PeopleContacts
                    .Where(
                        p => p.People.FirstName.Contains(search)           ||
                             p.People.LastName.Contains(search)            ||
                             p.People.MiddleName.Contains(search)          ||
                             p.People.Birthday.ToString().Contains(search) ||
                             p.People.Organization.Contains(search)        ||
                             p.People.Post.Contains(search)                ||
                             p.Value.Contains(search)
                    )
                    .OrderBy(p => p.People.LastName)
                    .ThenBy(p => p.People.FirstName)
                    .ThenBy(p => p.People.MiddleName);

                ViewBag.Search = search;
            }

            // Удаление дубликатов
            return View(contacts.GroupBy(p => p.PeopleID)
                                .Select(grp => grp.FirstOrDefault())
                                .ToList()
            );
        }

        public ActionResult People (int id = 0)
        {
            var people = context.Peoples.Find(id);
            if (people != null)
                return View(people);
            return Redirect("/Contacts");
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Types = GetContactTypes();
            return View(new People());
        }

        [HttpPost]
        public ActionResult Add(People people, List<int> ContactType, List<String> ContactValue)
        {
            if (ModelState.IsValid)
            {
                context.Peoples.Add(people);
                context.SaveChanges();
                var ID = context.Peoples.ToList().Last().PeopleID;

                for (int i = 0; i < ContactType.Count; ++i)
                {
                    if (string.IsNullOrEmpty(ContactValue[i])) continue;

                    var peopleContact = new PeopleContact
                    {
                        PeopleID = ID,
                        ContactID = ContactType[i],
                        Value = ContactValue[i]
                    };

                    context.PeopleContacts.Add(peopleContact);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (DbUpdateException)
                    {
                        context.PeopleContacts.Remove(peopleContact);
                        continue;
                    }
                }

                return Redirect("/Contacts/People/" + people.PeopleID);
            }

            ViewBag.Types = GetContactTypes();
            ViewBag.ContactType = ContactType;
            ViewBag.ContactValue = ContactValue;
            return View(people);
            
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            ViewBag.Types = GetContactTypes();

            var people = context.Peoples.Find(id);
            if (people != null)
                return View(people);
            return Redirect("/Contacts");
        }

        [HttpPost]
        public ActionResult Edit(People people, List<int> ContactType, List<String> ContactValue)
        {
            if (ModelState.IsValid)
            {
                var ID = people.PeopleID;
                context.Entry(people).State = EntityState.Modified;
                context.SaveChanges();

                // Удаление старых контактов
                var contacts = context.PeopleContacts.Where(p => p.PeopleID == ID).ToList();
                foreach (var contact in contacts)
                {
                    context.PeopleContacts.Remove(contact);
                }
                context.SaveChanges();

                // Добавление новыйх контактов
                for (int i = 0; i < ContactType.Count; ++i)
                {
                    if (string.IsNullOrEmpty(ContactValue[i])) continue;

                    var peopleContact = new PeopleContact();
                    peopleContact.PeopleID = ID;
                    peopleContact.ContactID = ContactType[i];
                    peopleContact.Value = ContactValue[i];

                    context.PeopleContacts.Add(peopleContact);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (DbUpdateException)
                    {
                        context.PeopleContacts.Remove(peopleContact);
                        continue;
                    }
                }

                return Redirect("/Contacts/People/" + people.PeopleID);
            }

            ViewBag.Types = GetContactTypes();
            ViewBag.ContactType = ContactType;
            ViewBag.ContactValue = ContactValue;
            return View(people);
            
        }

        public ActionResult Delete(int ID = 0)
        {
            var people = context.Peoples.Find(ID);
            if (people is null) return Redirect("/Contacts");

            var peopleContacts = context.PeopleContacts.Where(p => p.PeopleID == ID).ToList();
            foreach (var peopleContact in peopleContacts)
            {
                context.PeopleContacts.Remove(peopleContact);
            }

            context.Peoples.Remove(people);
            context.SaveChanges();

            return Redirect("/Contacts");
        }
    }
}