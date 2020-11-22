using ContactManager.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<Contact> Create(ContactViewModel contact);

        /// <summary>
        /// Delete contact
        /// </summary>
        Task<int> Delete(int id);

        /// <summary>
        /// Get all contacts
        /// </summary>
        Task<IEnumerable<Contact>> Get();

        /// <summary>
        /// Get contact
        /// </summary>
        Task<Contact> Get(int id);

        /// <summary>
        /// Update contact
        /// </summary>
        Task<Contact> Update(Contact contact);
    }
}