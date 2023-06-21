using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using FC12.SupportExtensions.Models;
using FC12.SupportExtensions.Outlook;

namespace Tests
{
    [TestClass]
    public class UnitTestOutlook
    {
        [TestMethod]
        public void TestCreateEmailSample()
        {
            OutlookHelper.CreateEmailSample(GlobalUsing.TestingRequest1);
            Assert.IsTrue(true);
        }
    }
}
