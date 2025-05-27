using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diana.Core.Host
{
    public interface IClassLoader
    {
        IEnumerable<Type> LoadCommands();
    }
}
