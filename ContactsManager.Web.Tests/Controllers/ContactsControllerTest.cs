using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ContactsManager.Domain.Entity;
using ContactsManager.Domain.Abstract;
using System.Collections.Generic;
using ContactsManager.Web.Controllers;
using System.Linq;

namespace ContactsManager.Web.Tests.Controllers
{
    [TestClass]
    public class ContactsControllerTest
    {
        [TestMethod]
        public void Can_Get_Values()
        {
            Mock<IContactRepository> mock = new Mock<IContactRepository>();
            mock.Setup(m => m.Get()).Returns(new List<Contact>
            {
                new Contact { Id = 0, LastName = "LastName 0" },
                new Contact { Id = 1, LastName = "LastName 1" },
                new Contact { Id = 2, LastName = "LastName 2" },
                new Contact { Id = 3, LastName = "LastName 3" },
                new Contact { Id = 4, LastName = "LastName 4" },
                new Contact { Id = 5, LastName = "LastName 5" },
                new Contact { Id = 6, LastName = "LastName 6" },
                new Contact { Id = 7, LastName = "LastName 7" },
                new Contact { Id = 8, LastName = "LastName 8" },
                new Contact { Id = 9, LastName = "LastName 9" },
                new Contact { Id = 10, LastName = "LastName 10" }
            });
            ContactsController controller = new ContactsController(mock.Object);

            var result = controller.Get();

            Assert.AreEqual(11, result.Count());
        }
    }
}
