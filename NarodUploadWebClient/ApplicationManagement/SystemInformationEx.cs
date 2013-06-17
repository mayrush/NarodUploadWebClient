using System;
using System.Management;
using System.Runtime.InteropServices;
using ApplicationManagement.WINAPI;

namespace ApplicationManagement
{
    /// <summary>
    /// Retrieve additional information from system.
    /// </summary>
    public static class SystemInformationEx
    {
        #region System Bits
        /// <summary>
        /// Check is the current system is running on 32Bits.
        /// </summary>
        /// <returns>Returns true if is 32bits, otherwise false.</returns>
        public static bool Is32Bits()
        {
            return IntPtr.Size == 4;
        }

        /// <summary>
        /// Check is the current system is running on 64Bits.
        /// </summary>
        /// <returns>Returns true if is 64bits, otherwise false.</returns>
        public static bool Is64Bits()
        {
            return IntPtr.Size == 8;
        }

        /// <summary>
        /// Gets the operating system bits.
        /// </summary>
        /// <returns>Returns 32 or 64 integer.</returns>
        public static int GetSystemBits()
        {
            return IntPtr.Size == 8 ? 64 : 32;
        }
        
        /// <summary>
        /// Gets a string with the operating system bits.
        /// </summary>
        /// <returns>Returns a string x86 if is 32bits, otherwise return x64.</returns>
        /// <remarks>x86 = 32bits and x64 = 64bits</remarks>
        public static string GetSystemBitsName()
        {
            return IntPtr.Size == 8 ? "x64" : "x86";
        }
        #endregion

        #region ServicePack
        /// <summary>
        /// Retrieve the current service pack installed. WinNT Only!
        /// </summary>
        /// <returns></returns>
        public static string GetServicePackValue()
        {
            return Environment.OSVersion.ServicePack;
            /*string sPack = string.Empty;
            Kernel32.OSVERSIONINFOEX versionInfo = new Kernel32.OSVERSIONINFOEX();

            versionInfo.dwOSVersionInfoSize = Marshal.SizeOf(typeof(Kernel32.OSVERSIONINFOEX));

            if (Kernel32.GetVersionEx(ref versionInfo))
                sPack = versionInfo.szCSDVersion;
            else
                return null;
            return sPack;*/
        }
        #endregion

        #region GetOSName
        /// <summary>
        /// Get the current operative system name.
        /// </summary>
        /// <param name="includeServicePack">If true, append ServicePack name to operating system name.</param>
        /// <param name="includeBits">If true, append system bits name to operating system name.</param>
        /// <returns>Returns a string containing the current operative system name.</returns>
        /// <remarks>Windows Vista ServicePack 1 x64</remarks>
        public static string GetOSName(bool includeServicePack, bool includeBits)
        {
            OperatingSystem os = Environment.OSVersion;
            string osName = "Unknown";
            switch (os.Platform)
            {
                case PlatformID.Win32Windows:
                    switch (os.Version.Minor)
                    {
                        case 0:
                            osName = "Windows 95";
                            break;
                        case 10:
                            osName = "Windows 98";
                            break;
                        case 90:
                            osName = "Windows ME";
                            break;
                    }
                    break;
                case PlatformID.Win32NT:
                    switch (os.Version.Major)
                    {
                        case 3:
                            osName = "Windws NT 3.51";
                            break;
                        case 4:
                            osName = "Windows NT 4";
                            break;
                        case 5:
                            switch (os.Version.Minor)
                            {
                                case 0:
                                    osName = "Windows 2000";
                                    break;
                                case 1:
                                    osName = "Windows XP";
                                    break;
                                case 2:
                                    osName = "Windows Server 2003";
                                    break;
                            }
                            break;
                        case 6:
                            osName = "Windows Vista";
                            switch (os.Version.Minor)
                            {
                                case 0:
                                    osName = "Windows Vista";
                                    break;
                                case 1:
                                    osName = "Windows 7";
                                    break;
                            }
                            break;
                    }
                    break;
                case PlatformID.MacOSX:
                    osName = "MacOSX";
                    break;
                case PlatformID.Unix:
                    osName = "Unix";
                    break;
                case PlatformID.WinCE:
                    osName = "WinCE";
                    break;
                case PlatformID.Xbox:
                    osName = "Xbox";
                    break;
            }
            if (includeServicePack)
                if(!Environment.OSVersion.ServicePack.Equals(string.Empty))
                    osName += string.Format(" {0}", Environment.OSVersion.ServicePack);
            if (includeBits)
                osName += string.Format(" {0}", GetSystemBitsName());
            return osName;
        }
        #endregion 

        #region GetProcessorCount
        /// <summary>
        /// Gets the total processores on the system.
        /// </summary>
        /// <returns>Returns the number of processors.</returns>
        public static int GetProcessorCount()
        {
            return Environment.ProcessorCount;
        }
        #endregion

        #region GetSystemInfo
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strTable"></param>
        /// <param name="strProperties"></param>
        /// <returns></returns>
        /// <example>GetSystemInfo("Win32_Processor", "Caption,Manufacturer");</example>
        public static string GetSystemInfo(string strTable, string strProperties)
        {
            try
            {
                string strInfo = string.Empty;
                using (ManagementObjectSearcher mos = new ManagementObjectSearcher())
                {
                    mos.Query.QueryString = "SELECT " + strProperties + " FROM " + strTable;
                    using(ManagementObjectCollection moc = mos.Get())
                    {
                        foreach (ManagementObject mo in moc)
                            foreach (PropertyData pd in mo.Properties)
                                strInfo += pd.Value + ",";
                    }
                }
                return strInfo.Substring(0, strInfo.Length - 1);
            }
            catch { return null; }
        }
        #endregion

    }
}