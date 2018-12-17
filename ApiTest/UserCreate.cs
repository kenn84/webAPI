using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTfull.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiTest
{
    [TestClass]
    public class UserCreate

    {



        UserController userController;
        [TestMethod]
        public void UserCreateTest()
        {

            userController = new UserController();


            HttpResponseMessage httpResponseMessage = userController.AddUser("sss", "ddd", "ddd", 76, DateTime.Now, 86, 78);
            bool test = httpResponseMessage.IsSuccessStatusCode;
            Assert.IsTrue(httpResponseMessage.IsSuccessStatusCode);

           //userController.DeleteUser("sss");



        }



    }
}
