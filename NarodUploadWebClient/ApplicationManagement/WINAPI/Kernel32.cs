using System;
using System.Runtime.InteropServices;

namespace ApplicationManagement.WINAPI
{
    /// <summary>
    /// Provide some Kernel32.dll functions. 
    /// </summary>
    public class Kernel32
    {
        #region DllImports

        #region SetProcessWorkingSetSize
        /// <summary>
        /// Sets the minimum and maximum working set sizes for the specified process.
        /// </summary>
        /// <param name="process">A handle to the process whose working set sizes is to be set. 
        /// 
        /// The handle must have the PROCESS_SET_QUOTA access right. 
        /// For more information, see Process Security and Access Rights.</param>
        /// <param name="minimumWorkingSetSize">The minimum working set size for the process, in bytes. 
        /// The virtual memory manager attempts to keep at least this much memory resident in the process whenever the process is active. 
        /// 
        /// This parameter must be greater than zero but less than or equal to the maximum working set size. 
        /// The default size is 50 pages (for example, this is 204,800 bytes on systems with a 4K page size). 
        /// If the value is greater than zero but less than 20 pages, the minimum value is set to 20 pages. 
        /// 
        /// If both dwMinimumWorkingSetSize and dwMaximumWorkingSetSize have the value (SIZE_T)–1, 
        /// the function removes as many pages as possible from the working set of the specified process.</param>
        /// <param name="maximumWorkingSetSize">The maximum working set size for the process, in bytes. 
        /// The virtual memory manager attempts to keep no more than this much memory resident in the process whenever 
        /// the process is active and available memory is low. 
        /// 
        /// This parameter must be greater than or equal to 13 pages (for example, 53,248 on systems with a 4K page size), 
        /// and less than the system-wide maximum (number of available pages minus 512 pages). 
        /// The default size is 345 pages (for example, this is 1,413,120 bytes on systems with a 4K page size). 
        /// 
        /// If both dwMinimumWorkingSetSize and dwMaximumWorkingSetSize have the value (SIZE_T)–1, 
        /// the function removes as many pages as possible from the working set of the specified process.</param>
        /// <returns>If the function succeeds, the return value is nonzero. 
        /// 
        /// If the function fails, the return value is zero. 
        /// Call GetLastError to obtain extended error information.</returns>
        /// <remarks>The working set of a process is the set of memory pages in the virtual 
        /// address space of the process that are currently resident in physical memory. 
        /// These pages are available for an application to use without triggering a page fault. 
        /// For more information about page faults, see Working Set. 
        /// The minimum and maximum working set sizes affect the virtual memory paging behavior of a process. 
        /// 
        /// The working set of the specified process can be emptied by specifying the value (SIZE_T)–1 
        /// for both the minimum and maximum working set sizes. This removes as many pages as possible from the working set. 
        /// The EmptyWorkingSet function can also be used for this purpose. 
        /// 
        /// If the values of either dwMinimumWorkingSetSize or dwMaximumWorkingSetSize are greater than the process' 
        /// current working set sizes, the specified process must have the SE_INC_WORKING_SET_NAME privilege. 
        /// All users generally have this privilege. For more information about security privileges, see Privileges. 
        /// 
        /// Windows Server 2003 and Windows XP/2000:  
        /// The specified process must have the SE_INC_BASE_PRIORITY_NAME privilege. 
        /// Users in the Administrators and Power Users groups generally have this privilege. 
        /// The operating system allocates working set sizes on a first-come, first-served basis. 
        /// For example, if an application successfully sets 40 megabytes as its minimum working set size on a 64-megabyte system, 
        /// and a second application requests a 40-megabyte working set size, the operating system denies the second application's request.
        /// 
        /// Using the SetProcessWorkingSetSize function to set an application's minimum and maximum working set sizes 
        /// does not guarantee that the requested memory will be reserved, or that it will remain resident at all times. 
        /// When the application is idle, or a low-memory situation causes a demand for memory, 
        /// the operating system can reduce the application's working set. 
        /// An application can use the VirtualLock function to lock ranges of the application's virtual address space in memory; 
        /// however, that can potentially degrade the performance of the system.
        /// 
        /// When you increase the working set size of an application, 
        /// you are taking away physical memory from the rest of the system. 
        /// This can degrade the performance of other applications and the system as a whole. 
        /// It can also lead to failures of operations that require physical memory to be present 
        /// (for example, creating processes, threads, and kernel pool). 
        /// Thus, you must use the SetProcessWorkingSetSize function carefully. 
        /// You must always consider the performance of the whole system when you are designing an application.</remarks>
        [DllImport("kernel32", EntryPoint = "SetProcessWorkingSetSize", ExactSpelling = true, SetLastError = false)]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);
        #endregion

        #region GetVersionEx

        /// <summary>
        /// Retrieves information about the current operating system. 
        /// ... Product Setting; Windows XP Media Center Edition: SM_MEDIACENTER: Windows XP Starter Edition
        /// </summary>
        /// <param name="osVersionInfo">An OSVERSIONINFO or OSVERSIONINFOEX structure that receives the operating system information. 
        /// 
        /// Before calling the GetVersionEx function, 
        /// set the dwOSVersionInfoSize member of the structure as appropriate 
        /// to indicate which data structure is being passed to this function.</param>
        /// <returns>If the function succeeds, the return value is a nonzero value. 
        /// 
        /// If the function fails, the return value is zero. 
        /// To get extended error information, call GetLastError. 
        /// The function fails if you specify an invalid value for the 
        /// dwOSVersionInfoSize member of the OSVERSIONINFO or OSVERSIONINFOEX structure.</returns>
        /// <remarks>Identifying the current operating system is usually not the best way to determine 
        /// whether a particular operating system feature is present. 
        /// This is because the operating system may have had new features added in a redistributable DLL. 
        /// Rather than using GetVersionEx to determine the operating system platform or version number, 
        /// test for the presence of the feature itself. For more information, see Operating System Version. 
        /// 
        /// The GetSystemMetrics function provides additional information about the current operating system. 
        /// 
        /// Product - Setting 
        /// Windows XP Media Center Edition - SM_MEDIACENTER 
        /// Windows XP Starter Edition - SM_STARTER 
        /// Windows XP Tablet PC Edition - SM_TABLETPC 
        /// Windows Server 2003 R2 - SM_SERVERR2
        /// 
        /// To check for specific operating systems or operating system features, use the IsOS function. 
        /// The GetProductInfo function retrieves the product type. 
        /// 
        /// To retrieve information for the operating system on a remote computer, 
        /// use the NetWkstaGetInfo function, the Win32_OperatingSystem WMI class, 
        /// or the OperatingSystem property of the IADsComputer interface. 
        /// 
        /// To compare the current system version to a required version, 
        /// use the VerifyVersionInfo function instead of using GetVersionEx to perform the comparison yourself. 
        /// 
        /// If compatibility mode is in effect, the GetVersionEx function reports the operating system as 
        /// it identifies itself, which may not be the operating system that is installed. 
        /// For example, if compatibility mode is in effect, GetVersionEx reports the operating system that 
        /// is selected for application compatibility.</remarks>
        [DllImport("kernel32.dll")]
        public static extern bool GetVersionEx(ref OSVERSIONINFOEX osVersionInfo);

        #endregion

        #endregion

        #region Structs

        #region OSVERSIONINFOEX
        /// <summary>
        /// Contains operating system version information. 
        /// The information includes major and minor version numbers, a build number, a platform identifier, 
        /// and information about product suites and the latest Service Pack installed on the system. 
        /// This structure is used with the GetVersionEx and VerifyVersionInfo functions.
        /// </summary>
        /// <remarks>Relying on version information is not the best way to test for a feature. 
        /// Instead, refer to the documentation for the feature of interest. 
        /// For more information on common techniques for feature detection, see Operating System Version. 
        /// 
        /// If you must require a particular operating system, be sure to use it as a minimum supported version, 
        /// rather than design the test for the one operating system. 
        /// This way, your detection code will continue to work on future versions of Windows. 
        /// 
        /// The following table summarizes the values returned by supported versions of Windows. 
        /// Use the information in the Other column to distinguish between operating systems with identical version numbers.</remarks>
        [StructLayout(LayoutKind.Sequential)]
        public struct OSVERSIONINFOEX
        {
            /// <summary>
            /// The size of this data structure, in bytes. Set this member to sizeof(OSVERSIONINFOEX).
            /// </summary>
            public int dwOSVersionInfoSize;
            /// <summary>
            /// The major version number of the operating system. For more information, see Remarks.
            /// </summary>
            public int dwMajorVersion;
            /// <summary>
            /// The minor version number of the operating system. For more information, see Remarks.
            /// </summary>
            public int dwMinorVersion;
            /// <summary>
            /// The build number of the operating system.
            /// </summary>
            public int dwBuildNumber;
            /// <summary>
            /// The operating system platform. This member can be VER_PLATFORM_WIN32_NT (2).
            /// </summary>
            public int dwPlatformId;
            /// <summary>
            /// A null-terminated string, such as "Service Pack 3", 
            /// that indicates the latest Service Pack installed on the system. 
            /// If no Service Pack has been installed, the string is empty.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCSDVersion;
            /// <summary>
            /// The major version number of the latest Service Pack installed on the system. 
            /// For example, for Service Pack 3, the major version number is 3. 
            /// If no Service Pack has been installed, the value is zero.
            /// </summary>
            public short wServicePackMajor;
            /// <summary>
            /// The minor version number of the latest Service Pack installed on the system. 
            /// For example, for Service Pack 3, the minor version number is 0.
            /// </summary>
            public short wServicePackMinor;
            /// <summary>
            /// A bit mask that identifies the product suites available on the system. 
            /// This member can be a combination of the following values. 
            /// 
            /// Value - Meaning
            /// 
            /// VER_SUITE_BACKOFFICE (0x00000004) - Microsoft BackOffice components are installed. 
            /// VER_SUITE_BLADE (0x00000400) - Windows Server 2003, Web Edition is installed. 
            /// VER_SUITE_COMPUTE_SERVER (0x00004000) - Windows Server 2003, Compute Cluster Edition is installed. 
            /// VER_SUITE_DATACENTER (0x00000080) - Windows Server 2008 Datacenter, Windows Server 2003, Datacenter Edition, or Windows 2000 Datacenter Server is installed.  
            /// VER_SUITE_ENTERPRISE (0x00000002) - Windows Server 2008 Enterprise, Windows Server 2003, Enterprise Edition, or Windows 2000 Advanced Server is installed. Refer to the Remarks section for more information about this bit flag. 
            /// VER_SUITE_EMBEDDEDNT (0x00000040) - Windows XP Embedded is installed. 
            /// VER_SUITE_PERSONAL (0x00000200) - Windows Vista Home Premium, Windows Vista Home Basic, or Windows XP Home Edition is installed. 
            /// VER_SUITE_SINGLEUSERTS (0x00000100) - Remote Desktop is supported, but only one interactive session is supported. This value is set unless the system is running in application server mode. 
            /// VER_SUITE_SMALLBUSINESS (0x00000001) - Microsoft Small Business Server was once installed on the system, but may have been upgraded to another version of Windows. Refer to the Remarks section for more information about this bit flag. 
            /// VER_SUITE_SMALLBUSINESS_RESTRICTED (0x00000020) - Microsoft Small Business Server is installed with the restrictive client license in force. Refer to the Remarks section for more information about this bit flag. 
            /// VER_SUITE_STORAGE_SERVER (0x00002000) - Windows Storage Server 2003 R2 or Windows Storage Server 2003is installed. 
            /// VER_SUITE_TERMINAL (0x00000010) - Terminal Services is installed. This value is always set. 
            /// If VER_SUITE_TERMINAL is set but VER_SUITE_SINGLEUSERTS is not set, the system is running in application server mode. 
            /// VER_SUITE_WH_SERVER (0x00008000) - Windows Home Server is installed.
            /// </summary>
            public short wSuiteMask;
            /// <summary>
            /// Any additional information about the system. This member can be one of the following values. 
            /// 
            /// Value - Meaning
            /// 
            /// VER_NT_DOMAIN_CONTROLLER (0x0000002) - The system is a domain controller and the operating system is Windows Server 2008, Windows Server 2003, or Windows 2000 Server. 
            /// VER_NT_SERVER (0x0000003) - The operating system is Windows Server 2008, Windows Server 2003, or Windows 2000 Server. 
            /// Note that a server that is also a domain controller is reported as VER_NT_DOMAIN_CONTROLLER, not VER_NT_SERVER. 
            /// VER_NT_WORKSTATION (0x0000001) - The operating system is Windows Vista, Windows XP Professional, Windows XP Home Edition, or Windows 2000 Professional.
            /// </summary>
            public byte wProductType;
            /// <summary>
            /// Reserved for future use.
            /// </summary>
            public byte wReserved;
        }

        #endregion

        #endregion
    }
}