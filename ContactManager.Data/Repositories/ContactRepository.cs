using ContactManager.Entities.Models;
using ContactManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactManager.Data.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private ContactDbContext _context;

        public ContactRepository(ContactDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all contact
        /// </summary>
        public IEnumerable<Contact> Get()
        {
            try
            {
                var contacts = _context.Contacts.ToList();

                return contacts;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Get contact by id
        /// </summary>
        public Contact Get(int id)
        {
            var contact = _context.Contacts.Find(id);

            return contact;
        }

        /// <summary>
        /// Create new contact
        /// </summary>
        public Contact Create(ContactViewModel contact)
        {
            var contactModel = ContactToModel(contact);
            _context.Contacts.Add(contactModel);
            _context.SaveChanges();

            return contactModel;
        }

        /// <summary>
        /// Update contact
        /// </summary>
        public Contact Update(Contact contact)
        {
            _context.Contacts.Update(contact);
            _context.SaveChanges();

            return contact;
        }

        /// <summary>
        /// Delete contact
        /// </summary>
        public int Delete(int id)
        {
            var contactModel = _context.Contacts.Find(id);
            _context.Contacts.Remove(contactModel);
            _context.SaveChanges();
            return id;
        }

        /// <summary>
        /// Convert viewModel to model
        /// </summary>
        public Contact ContactToModel(ContactViewModel contact)
        {
            var model = new Contact()
            {
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                PhoneNumber = contact.PhoneNumber,
                Status = contact.Status
            };

            return model;
        }
    }
}
