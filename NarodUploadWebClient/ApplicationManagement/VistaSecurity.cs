using System;
using System.Diagnostics;
using System.Security.Principal;

namespace ApplicationManagement
{
    /// <summary>
    /// Functions relative to Windows Vista and/or above.
    /// </summary>
    public static class VistaSecurity
    {
        #region IsAdmin
        /// <summary>
        /// Check if current user is an administrator.
        /// </summary>
        /// <returns>Returns true if user is an administrator, otherwise false.</returns>
        public static bool IsAdmin()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal p = new WindowsPrincipal(id);
            return p.IsInRole(WindowsBuiltInRole.Administrator);
        }
        #endregion

        #region RestartElevated

        /// <summary>
        /// Restart application with elevated permitions.
        /// </summary>
        /// <param name="app">Path to application executable, use: Application.ExecutablePath</param>
        /// <returns>Returns true if all went fine, otherwise false.</returns>
        /// <remarks>You need kill your application if this returns true, be call: Application.Exit();</remarks>
        /// <example>
        /// if(!VistaSecurity.IsAdmin())
        /// {
        ///     if(VistaSecurity.RestartElevated(Application.ExecutablePath))
        ///         Application.Exit();
        /// }
        /// </example>
        public static bool RestartElevated(string app)
        {
            using (Process proc = new Process())
            {
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                proc.StartInfo.FileName = app;
                proc.StartInfo.Verb = "runas";
                try
                {
                    proc.Start();
                    proc.Close();
                }
                catch (System.ComponentModel.Win32Exception)
                {
                    return false;
                }
            }
            return true;
        }

        #endregion
    }
}