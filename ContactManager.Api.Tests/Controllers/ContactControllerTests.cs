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
using System.ComponentModel.DataAnnotations;

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
        public void GetContacts(JObject output)
        {
            //Arrange
            var contact = JsonConvert.DeserializeObject<Contact>(output.ToString());
            List<Contact> contacts = new List<Contact>();
            contacts.Add(contact);

            var repository = new Mock<IContactRepository>();
            repository.Setup(x => x.Get()).Returns(contacts);

            ContactController controller = new ContactController(repository.Object);

            //Act
            var response = controller.Get();

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
        public void GetContactById(int id, JObject output)
        {
            //Arrange
            var contact = JsonConvert.DeserializeObject<Contact>(output.ToString());

            var repository = new Mock<IContactRepository>();
            repository.Setup(x => x.Get(It.IsAny<int>())).Returns(contact);

            ContactController controller = new ContactController(repository.Object);

            //Act
            var response = controller.Get(id);

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
        public void CreateContact(JObject input, JObject output)
        {
            //Arrange
            var contactViewModel = JsonConvert.DeserializeObject<ContactViewModel>(input.ToString());
            var contact = JsonConvert.DeserializeObject<Contact>(output.ToString());

            var repository = new Mock<IContactRepository>();
            repository.Setup(x => x.Create(It.IsAny<ContactViewModel>())).Returns(contact);

            ContactController controller = new ContactController(repository.Object);

            //Act
            var response = controller.Post(contactViewModel);

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
        public void UpdateContact(JObject input, JObject output)
        {
            //Arrange
            var request = JsonConvert.DeserializeObject<Contact>(input.ToString());
            var contact = JsonConvert.DeserializeObject<Contact>(output.ToString());

            var repository = new Mock<IContactRepository>();
            repository.Setup(x => x.Update(It.IsAny<Contact>())).Returns(contact);

            ContactController controller = new ContactController(repository.Object);

            //Act
            var response = controller.Put(request);

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
        public void DeleteContact(int id)
        {
            //Arrange

            var repository = new Mock<IContactRepository>();
            repository.Setup(x => x.Delete(It.IsAny<int>())).Returns(id);

            ContactController controller = new ContactController(repository.Object);

            //Act
            var response = controller.Delete(id);

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
