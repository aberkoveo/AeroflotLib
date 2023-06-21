using ABBYY.FlexiCapture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC12
{
    public static class Utilities
    {
        public static string GetEnvironmentVariable(string variableName, IProject project)
        {
            try { return project.EnvironmentVariables.Get(variableName); }
            catch (System.Exception e) { throw new System.Exception(e.Message + "\n" + e.StackTrace); }
        }
    }
}
