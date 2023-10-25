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
            if (project.EnvironmentVariables.Has(variableName))
            {
                return project.EnvironmentVariables.Get(variableName);
            }
            else
            {
                throw new Exception($"Project environment variable {variableName} is not defined");
            }
        }
    }
}
