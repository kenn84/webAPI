using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTfull.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace ApiTest
{
    [TestClass]
    public class UserGet {
        [TestMethod]
        public void UserGetTest()

    {
        UserController userController = new UserController();
        userController.AddUser("jdjdj", "djjd", "efkh", 73, DateTime.Now, 33, 2);
        IHttpActionResult httpActionResult = userController.GetUser("jdjdj");
        var contentResult = httpActionResult as OkNegotiatedContentResult<object>;

        // Assert
        Assert.IsNotNull(contentResult);
        Assert.IsNotNull(contentResult.Content);


        userController.DeleteUser("jdjdj");

    }
}
}
