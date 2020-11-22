using ContactManager.Entities.Models;
using ContactManager.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IEnumerable<Contact>> Get()
        {
            try
            {
                var contacts = await _context.Contacts.ToListAsync();

                return (IEnumerable<Contact>)contacts;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Get contact by id
        /// </summary>
        public async Task<Contact> Get(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            return contact;
        }

        /// <summary>
        /// Create new contact
        /// </summary>
        public async Task<Contact> Create(ContactViewModel contact)
        {
            var contactModel = ContactToModel(contact);
            _context.Contacts.Add(contactModel);
            await _context.SaveChangesAsync();

            return contactModel;
        }

        /// <summary>
        /// Update contact
        /// </summary>
        public async Task<Contact> Update(Contact contact)
        {
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();

            return contact;
        }

        /// <summary>
        /// Delete contact
        /// </summary>
        public async Task<int> Delete(int id)
        {
            var contactModel = await _context.Contacts.FindAsync(id);
            _context.Contacts.Remove(contactModel);
            await _context.SaveChangesAsync();
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
