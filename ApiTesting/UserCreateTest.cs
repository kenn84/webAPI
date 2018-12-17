using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTested.WebApi;

using MyApp.Controllers;
using NUnit.Framework;
using RESTfull.Controllers;

namespace ApiTesting
{
    [TestFixture]
    public class UserCreateTest
    {


        [Test]
        public void ReturnOkWhenCallingGetAction()
        {
            MyWebApi
                .Controller<UserController>()
                .Calling(c => c.Get())
                .ShouldReturn()
                .Ok();
        }


    }
}
