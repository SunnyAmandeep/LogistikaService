 
namespace Logistika.Service.Common.Ioc
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Represents a helper class for assemblies management
    /// </summary>
    public static class AssemblyHelper
    {
        private static readonly List<Assembly> AvailableAssemblyCache = new List<Assembly>();

        private static string AssemblyDirectory
        {
            get { return Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path)); }
        }

        public static IList<Assembly> GetAvailableAssemblies()
        {
            lock (AvailableAssemblyCache)
            {
                if (AvailableAssemblyCache.Count == 0)
                {
                    foreach (var file in Directory.GetFiles(AssemblyDirectory, "*.dll"))
                    {
                        AvailableAssemblyCache.Add(Assembly.LoadFrom(file));
                    }
                }
            }

            return AvailableAssemblyCache;
        }
    }
}