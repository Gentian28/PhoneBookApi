using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using PhoneBookApi;
using PhoneBookApi.Controllers;
using PhoneBookApi.Models;
using System;

namespace ControllerTests
{
    [TestFixture]
    public class ControllerTestClass
    {
        [Test]
        public void GetPhoneBookItems_ReturnsOk()
        {
            // arrange
            var controller = new PhoneBookController();

            // act
            var result = controller.GetPhoneBookItems();
            var okResult = result as OkObjectResult;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void GetPhoneBookItem_ReturnsOk()
        {
            // arrange
            var controller = new PhoneBookController();

            // act
            var result = controller.GetPhoneBookItem(4);
            var okResult = result as OkObjectResult;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
    }
}