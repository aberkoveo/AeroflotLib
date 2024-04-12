using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FC12.SupportExtensions;
using System.Windows.Forms;
using FC12.SupportExtensions.Models;


namespace Tests
{
    [TestClass]
    public class UnitTestWindowsForm
    {
        [TestMethod]
        [STAThread]
        public void TestWindowsForm()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SupportMessageForm(GlobalUsing.TestingRequest2, null, true));
        }

        [TestMethod]
        [STAThread]
        public void TestWindowsFormWithEmail()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SupportMessageForm(GlobalUsing.TestingRequest2));

        }


    }
}
