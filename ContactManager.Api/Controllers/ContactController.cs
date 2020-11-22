using System;
using ContactManager.Entities.Models;
using ContactManager.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _repository;

        public ContactController(IContactRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets a list of all contacts
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var contacts = _repository.Get();

                return Ok(contacts);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Get a specific contact by id
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var contact = _repository.Get(id);
                if (contact is null)
                    return NotFound();
                return Ok(contact);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
       

        /// <summary>
        /// Creates a new contact
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /
        ///     {
        ///         "firstName": "Rajesh",
        ///         "lastName": "Kumar",
        ///         "email": "abc@gmail.com",
        ///         "phoneNumber": "0000000000",
        ///         "status": "active"
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        public IActionResult Post([FromBody] ContactViewModel contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var createdContact = _repository.Create(contact);
                    return Created("newContact", createdContact);
                }
                return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Updates contact information
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /
        ///    {
        ///         "contactId": 1,
        ///         "firstName": "Rajesh",
        ///         "lastName": "Kumar",
        ///         "email": "abc@gmail.com",
        ///         "phoneNumber": "1111111111",
        ///         "status": "active"
        ///     }
        ///
        /// </remarks>
        [HttpPut]
        public IActionResult Put([FromBody] Contact contact)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(contact);
                return Ok(contact);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Delete a contact by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>HttpStatusCode 200</returns>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                _repository.Delete(id);
                return Ok(id);
            }
            return BadRequest(ModelState);
        }
    }
}
