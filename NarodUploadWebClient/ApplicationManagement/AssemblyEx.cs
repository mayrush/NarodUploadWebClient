using System;
using System.Reflection;

namespace ApplicationManagement
{
    /// <summary>
    /// Provide utilities relationed with assembly.
    /// </summary>
    public static class AssemblyEx
    {
        /// <summary>
        /// Get a list with the loaded assemblies.
        /// </summary>
        /// <returns>Returns a list.</returns>
        public static Assembly[] GetLoadedAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }
    }
}