using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NextFramework
{
    public interface IApplication
    {
        Task ReloadAsync();
    }
}
