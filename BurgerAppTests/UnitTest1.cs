using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBaseContext;
using DataBaseContext.Entities;
using DataBaseContext.Security;
using DataBaseContext.random;
namespace BurgerAppTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddAddressToDataBaseTest()
        {
            Assert.IsTrue(DataBaseQuery.AddAddressToDataBase(RandomAddress.Generate()));
        }
        [TestMethod]
        public void AddRestaurantToDataBase()
        {
            Assert.IsTrue(DataBaseQuery.AddRestaurantToDataBase(RandomRestaurant.Generate()));
        }
        [TestMethod]
        public void AddStaffToDataBase()
        {
            Assert.IsTrue(DataBaseQuery.AddStaffToDataBase(RandomStaff.Generate()));
        }
        
        [TestMethod]
        public void AddCustomerToDataBase()
        {
            Assert.IsTrue(DataBaseQuery.AddCustomerToDataBase(RandomCustomer.Generate()));
        }

        
        [TestMethod]
        public void AddOrderToDataBase()
        {
            Assert.IsTrue(DataBaseQuery.AddOrderToDataBase(RandomOrder.Generate()));
        }
       
        [TestMethod]
        public void AddProduct_OrderToDataBase()
        {
            Assert.IsTrue(DataBaseQuery.AddProduct_OrderToDataBase(RandomProduct_Order.Generate()));
        }
        [TestMethod]
        [DataRow("haslo")]
        [DataRow("mocnehaslo123")]
        [DataRow("qwerty")]
        [DataRow("!@QWZXZ")]
        public void EncryptionTest(string text)
        {
            var en = Encryption.ComputeHash(text, "SHA512", null);
            Assert.IsTrue(Encryption.VerifyHash(text, "SHA512", en));
        }



    }
}
