using ContactManager.Api.Controllers;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using ContactManager.Interfaces;
using ContactManager.Common.Attributes;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ContactManager.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using ContactManager.Common.Constants;

namespace ContactManager.Api.Tests.Controllers
{
    public class ContactControllerTests
    {
        public const string TestData = @"TestData\TestData.Json";

        /// <summary>
        /// Test Get method
        /// </summary>
        /// <param name="input"></param>
        [Theory]
        [JsonFileDataAttribute(TestData, "GetContacts")]
        public async Task GetContacts(JObject output)
        {
            //Arrange
            var contact = JsonConvert.DeserializeObject<Contact>(output.ToString());
            List<Contact> contacts = new List<Contact>();
            contacts.Add(contact);

            var repository = new Mock<IContactRepository>();
            repository.Setup(x => x.Get()).ReturnsAsync(contacts);

            ContactController controller = new ContactController(repository.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            controller.ControllerContext.HttpContext.Request.Headers.Add(ContactManagerConstatants.ClientId, "12345");

            //Act
            var response = await controller.Get();

            //Assert
            var result = ((IEnumerable<Contact>)((ObjectResult)response).Value);
            Assert.True(result.Count() > 0);
        }

        /// <summary>
        /// Test Get by id method
        /// </summary>
        /// <param name="input"></param>
        [Theory]
        [JsonFileDataAttribute(TestData, "GetContactById")]
        public async Task GetContactById(int id, JObject output)
        {
            //Arrange
            var contact = JsonConvert.DeserializeObject<Contact>(output.ToString());

            var repository = new Mock<IContactRepository>();
            repository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(contact);

            ContactController controller = new ContactController(repository.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            controller.ControllerContext.HttpContext.Request.Headers.Add(ContactManagerConstatants.ClientId, "12345");

            //Act
            var response = await controller.Get(id);

            //Assert
            var result = ((Contact)((ObjectResult)response).Value);
            Assert.NotNull(result);
        }

        /// <summary>
        /// Test input validations
        /// </summary>
        /// <param name="input"></param>
        [Theory]
        [JsonFileDataAttribute(TestData, "InputValidationError")]
        public void InputValidation(JObject input, string invalidPropertyName)
        {
            //Arrange
            var contact = JsonConvert.DeserializeObject<ContactViewModel>(input.ToString());

            //Act
            var result = ValidateModel(contact);

            //Assert
            Assert.True(result.First().MemberNames.First() == invalidPropertyName);
        }

        /// <summary>
        /// Test Post method
        /// </summary>
        /// <param name="input"></param>
        [Theory]
        [JsonFileDataAttribute(TestData, "CreateContact")]
        public async Task CreateContact(JObject input, JObject output)
        {
            //Arrange
            var contactViewModel = JsonConvert.DeserializeObject<ContactViewModel>(input.ToString());
            var contact = JsonConvert.DeserializeObject<Contact>(output.ToString());

            var repository = new Mock<IContactRepository>();
            repository.Setup(x => x.Create(It.IsAny<ContactViewModel>())).ReturnsAsync(contact);

            ContactController controller = new ContactController(repository.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            controller.ControllerContext.HttpContext.Request.Headers.Add(ContactManagerConstatants.ClientId, "12345");

            //Act
            var response = await controller.Post(contactViewModel);

            //Assert
            var result = ((Contact)((ObjectResult)response).Value);
            Assert.NotNull(result);
        }

        /// <summary>
        /// Test Put method
        /// </summary>
        /// <param name="input"></param>
        [Theory]
        [JsonFileDataAttribute(TestData, "UpdateContact")]
        public async Task UpdateContact(JObject input, JObject output)
        {
            //Arrange
            var request = JsonConvert.DeserializeObject<Contact>(input.ToString());
            var contact = JsonConvert.DeserializeObject<Contact>(output.ToString());

            var repository = new Mock<IContactRepository>();
            repository.Setup(x => x.Update(It.IsAny<Contact>())).ReturnsAsync(contact);

            ContactController controller = new ContactController(repository.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            controller.ControllerContext.HttpContext.Request.Headers.Add(ContactManagerConstatants.ClientId, "12345");

            //Act
            var response = await controller.Put(request);

            //Assert
            var result = ((Contact)((ObjectResult)response).Value);
            Assert.NotNull(result);
        }

        /// <summary>
        /// Test Delete method
        /// </summary>
        /// <param name="input"></param>
        [Theory]
        [JsonFileDataAttribute(TestData, "DeleteContact")]
        public async Task DeleteContact(int id)
        {
            //Arrange

            var repository = new Mock<IContactRepository>();
            repository.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(id);

            ContactController controller = new ContactController(repository.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            controller.ControllerContext.HttpContext.Request.Headers.Add(ContactManagerConstatants.ClientId, "12345");

            //Act
            var response = await controller.Delete(id);

            //Assert
            var result = ((int)((ObjectResult)response).Value);
            Assert.Equal(id,result);
        }

        /// <summary>
        /// method to validate model.
        /// </summary>
        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}
