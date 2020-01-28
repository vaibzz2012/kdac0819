using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySecurityLib2;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string result = Security.GetOTP("1", 5);
            Assert.AreNotEqual(string.Empty, result);
        }
        [TestMethod]
        public void TestMethod2()
        {
            string pwd = "Sunbeam";
            string encData = null;
            bool result = Security.Encrypt(pwd, out encData);
            Assert.AreEqual(true, result);

        }

        //[TestMethod]
        //public void TestMethod3()
        //{
        //    string dc = "vHK4GrTDbOc";
        //    string plain = null;
        //    Security.Decrypt(dc, out plain);
        //    Assert.AreEqual(true, plain);
        //}
    }
}