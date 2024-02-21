using FC12.SupportExtensions.Outlook;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FC12.SupportingTasks;


namespace Tests
{
        [TestClass]
        public class ProcessKillingTests
        {
            [TestMethod]
            public void TestKillProcesses()
            {
                WindowsProcessHandler.KillWindowsProcess();
            }
        }
}
