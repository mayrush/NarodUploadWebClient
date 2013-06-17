using System;
using System.Runtime.InteropServices;

namespace ApplicationManagement.WINAPI
{
    /// <summary>
    /// Provide some Dbghelp.dll functions. 
    /// </summary>
    public static class Dbghelp
    {
        #region MINIDUMP_TYPE Enum
        /// <summary>
        /// Identifies the type of information that will be written to the minidump file by the MiniDumpWriteDump function.
        /// </summary>
        [Flags]
        public enum MINIDUMP_TYPE
        {
            /// <summary>
            /// Include just the information necessary to capture stack traces for all existing threads in a process.
            /// </summary>
            MiniDumpNormal = 0x00000000,
            /// <summary>
            /// Include the data sections from all loaded modules. 
            /// This results in the inclusion of global variables, which can make the minidump file significantly larger. 
            /// For per-module control, use the ModuleWriteDataSeg enumeration value from MODULE_WRITE_FLAGS.
            /// </summary>
            MiniDumpWithDataSegs = 0x00000001,
            /// <summary>
            /// Include all accessible memory in the process. 
            /// The raw memory data is included at the end, so that the initial structures can be 
            /// mapped directly without the raw memory information. This option can result in a very large file.
            /// </summary>
            MiniDumpWithFullMemory = 0x00000002,
            /// <summary>
            /// Include high-level information about the operating system handles that are active when the minidump is made.
            /// </summary>
            MiniDumpWithHandleData = 0x00000004,
            /// <summary>
            /// Stack and backing store memory written to the minidump file should be filtered to remove all 
            /// but the pointer values necessary to reconstruct a stack trace. Typically, this removes any private information.
            /// </summary>
            MiniDumpFilterMemory = 0x00000008,
            /// <summary>
            /// Stack and backing store memory should be scanned for pointer references to modules in the module list. 
            /// If a module is referenced by stack or backing store memory, 
            /// the ModuleWriteFlags member of the MINIDUMP_CALLBACK_OUTPUT structure is set to ModuleReferencedByMemory.
            /// </summary>
            MiniDumpScanMemory = 0x00000010,
            /// <summary>
            /// Include information from the list of modules that were recently unloaded, 
            /// if this information is maintained by the operating system.
            /// 
            /// Windows Server 2003 and Windows XP:  
            /// The operating system does not maintain information for unloaded modules until 
            /// Windows Server 2003 with SP1 and Windows XP with SP2.
            /// DbgHelp 5.1:  This value is not supported.
            /// </summary>
            MiniDumpWithUnloadedModules = 0x00000020,
            /// <summary>
            /// Include pages with data referenced by locals or other stack memory. 
            /// This option can increase the size of the minidump file significantly.
            /// 
            /// DbgHelp 5.1:  This value is not supported.
            /// </summary>
            MiniDumpWithIndirectlyReferencedMemory = 0x00000040,
            /// <summary>
            /// Filter module paths for information such as user names or important directories. This option may prevent the system from locating the image file and should be used only in special situations.
            /// 
            /// DbgHelp 5.1:  This value is not supported.
            /// </summary>
            MiniDumpFilterModulePaths = 0x00000080,
            /// <summary>
            /// Include complete per-process and per-thread information from the operating system. 
            /// 
            /// DbgHelp 5.1:  This value is not supported.
            /// </summary>
            MiniDumpWithProcessThreadData = 0x00000100,
            /// <summary>
            /// Scan the virtual address space for PAGE_READWRITE memory to be included. 
            /// 
            /// DbgHelp 5.1:  This value is not supported.
            /// </summary>
            MiniDumpWithPrivateReadWriteMemory = 0x00000200,
            /// <summary>
            /// Reduce the data that is dumped by eliminating memory regions that are not essential 
            /// to meet criteria specified for the dump. This can avoid dumping memory that may contain 
            /// data that is private to the user. However, it is not a guarantee that no private information will be present. 
            /// 
            /// DbgHelp 6.1 and earlier:  This value is not supported.
            /// </summary>
            MiniDumpWithoutOptionalData = 0x00000400,
            /// <summary>
            /// Include memory region information. For more information, see MINIDUMP_MEMORY_INFO_LIST. 
            /// 
            /// DbgHelp 6.1 and earlier:  This value is not supported.
            /// </summary>
            MiniDumpWithFullMemoryInfo = 0x00000800,
            /// <summary>
            /// Include thread state information. For more information, see MINIDUMP_THREAD_INFO_LIST. 
            /// 
            /// DbgHelp 6.1 and earlier:  This value is not supported.
            /// </summary>
            MiniDumpWithThreadInfo = 0x00001000,
            /// <summary>
            /// Include all code and code-related sections from loaded modules to capture executable content. 
            /// For per-module control, use the ModuleWriteCodeSegs enumeration value from MODULE_WRITE_FLAGS. 
            /// 
            /// DbgHelp 6.1 and earlier:  This value is not supported.
            /// </summary>
            MiniDumpWithCodeSegs = 0x00002000,
            /// <summary>
            /// Turns off secondary auxiliary-supported memory gathering.
            /// </summary>
            MiniDumpWithoutAuxiliaryState = 0x00004000,
            /// <summary>
            /// Requests that auxiliary data providers include their state in the dump image; 
            /// the state data that is included is provider dependent. This option can result in a large dump image.
            /// </summary>
            MiniDumpWithFullAuxiliaryState = 0x00008000,
            /// <summary>
            /// Scans the virtual address space for PAGE_WRITECOPY memory to be included. 
            /// 
            /// Prior to DbgHelp 6.1:  This value is not supported.
            /// </summary>
            MiniDumpWithPrivateWriteCopyMemory = 0x00010000,
            /// <summary>
            /// If you specify MiniDumpWithFullMemory, the MiniDumpWriteDump function will fail if the function cannot read 
            /// the memory regions; however, if you include MiniDumpIgnoreInaccessibleMemory, 
            /// the MiniDumpWriteDump function will ignore the memory read failures and continue to generate the dump. 
            /// Note that the inaccessible memory regions are not included in the dump.
            /// 
            /// Prior to DbgHelp 6.1:  This value is not supported.
            /// </summary>
            MiniDumpIgnoreInaccessibleMemory = 0x00020000,
            /// <summary>
            /// Adds security token related data. This will make the "!token" extension work when processing a user-mode dump. 
            /// 
            /// Prior to DbgHelp 6.1:  This value is not supported.
            /// </summary>
            MiniDumpWithTokenInformation = 0x00040000
        }
        #endregion

        #region DllImports
        #region MiniDumpWriteDump
        /// <summary>
        /// Writes user-mode minidump information to the specified file.
        /// </summary>
        /// <param name="hProcess">A handle to the process for which the information is to be generated. 
        /// 
        /// This handle must have PROCESS_QUERY_INFORMATION and PROCESS_VM_READ access to the process. 
        /// For more information, see Process Security and Access Rights. 
        /// The caller must also be able to get THREAD_ALL_ACCESS access to the threads in the process. 
        /// For more information, see Thread Security and Access Rights.</param>
        /// <param name="ProcessId">The identifier of the process for which the information is to be generated.</param>
        /// <param name="hFile">A handle to the file in which the information is to be written.</param>
        /// <param name="DumpType">The type of information to be generated. This parameter can be one or more of the values from the MINIDUMP_TYPE enumeration.</param>
        /// <param name="ExceptionParam">A pointer to a MINIDUMP_EXCEPTION_INFORMATION structure 
        /// describing the client exception that caused the minidump to be generated. 
        /// If the value of this parameter is NULL, no exception information is included in the minidump file.</param>
        /// <param name="UserStreamParam">A pointer to a MINIDUMP_USER_STREAM_INFORMATION structure. 
        /// If the value of this parameter is NULL, no user-defined information is included in the minidump file.</param>
        /// <param name="CallackParam">A pointer to a MINIDUMP_CALLBACK_INFORMATION structure that specifies a callback routine 
        /// which is to receive extended minidump information. 
        /// If the value of this parameter is NULL, no callbacks are performed.</param>
        /// <returns>If the function succeeds, the return value is TRUE; otherwise, the return value is FALSE. 
        /// To retrieve extended error information, call GetLastError. Note that the last error will be an HRESULT value.
        /// 
        /// If the operation is canceled, the last error code is HRESULT_FROM_WIN32(ERROR_CANCELLED).</returns>
        /// <remarks>The MiniDumpCallback function receives extended minidump information from MiniDumpWriteDump. 
        /// It also provides a way for the caller to determine the granularity of information written 
        /// to the minidump file, as the callback function can filter the default information.
        /// 
        /// MiniDumpWriteDump may not produce a valid stack trace for the calling thread. 
        /// To work around this problem, you must capture the state of the calling thread before 
        /// calling MiniDumpWriteDump and use it as the ExceptionParam parameter. 
        /// One way to do this is to force an exception inside a __try/__except block and use the EXCEPTION_POINTERS 
        /// information provided by GetExceptionInformation. 
        /// Alternatively, you can call the function from a 
        /// new worker thread and filter this worker thread from the dump.</remarks>
        [DllImport("dbghelp.dll")]
        public static extern bool MiniDumpWriteDump(
            IntPtr hProcess,
            Int32 ProcessId,
            IntPtr hFile,
            MINIDUMP_TYPE DumpType,
            IntPtr ExceptionParam,
            IntPtr UserStreamParam,
            IntPtr CallackParam);
        #endregion
        #endregion
    }
}