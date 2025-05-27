using Diana.Core.Host;
using System.Reflection;

namespace Diana.Core.Defaults
{
    public class DefaultClassLoader : IClassLoader
    {
        public IEnumerable<Type> LoadCommands()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            return assembly.GetTypes()
                .Where(t => t.IsClass && t.IsAssignableTo(typeof(ICommand)))
                .AsEnumerable();
        }
    }
}
