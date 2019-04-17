using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NextFramework.Scripting
{
    public class ApplicationWrapper : IApplication
    {
        public Task ReloadAsync()
        {
            return Application.ReloadAsync();
        }
    }
}
