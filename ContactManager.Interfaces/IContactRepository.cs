using ContactManager.Entities.Models;
using System.Collections.Generic;

namespace ContactManager.Interfaces
{
    /// <summary>
    /// Contact repository
    /// </summary>
    public interface IContactRepository
    {
        /// <summary>
        /// Create new contact
        /// </summary>
        Contact Create(ContactViewModel contact);

        /// <summary>
        /// Delete contact
        /// </summary>
        int Delete(int id);

        /// <summary>
        /// Get all contacts
        /// </summary>
        IEnumerable<Contact> Get();

        /// <summary>
        /// Get contact
        /// </summary>
        Contact Get(int id);

        /// <summary>
        /// Update contact
        /// </summary>
        Contact Update(Contact contact);
    }
}