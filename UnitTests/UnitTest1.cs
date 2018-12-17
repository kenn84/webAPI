using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTfull.Controllers;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        UserController userController;
        [TestMethod]
        public void TestMethod1()
        {
            userController = new UserController();
            //HttpResponseMessage httpResponseMessage = userController.AddUser("jdjdj", "djjd", "efkh", 73, DateTime.Now, 33, 2);
            //Assert.IsTrue(httpResponseMessage.IsSuccessStatusCode);

            //userController.DeleteUser("jdjdj");

            IHttpActionResult httpActionResult = userController.GetUser("444");
            var contentResult = httpActionResult as OkNegotiatedContentResult<object>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);


        }
    }
}
