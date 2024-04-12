

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CredentialManagement;


namespace Integra.Persistence.CredentialManager
{
    public interface IWindowsCredentialLoader
    {
        Credential Load();
    }
}
