using System;
using DictaFoule.Mobile.iOS.Business;
using NUnit.Framework;
namespace Dictafoule.Mobile.IOS.Test
{
    [TestFixture]
    public class UserTest
    {
        [Test]
        public void TestUserCreateUser()
        {
            var user = new User();
            Assert.DoesNotThrow(() => user.CreateUser());
            Assert.DoesNotThrow(() => user.GetUser());
        }
    }
}
