using System;
using System.Diagnostics;
using System.IO;
using ApplicationManagement.WINAPI;

namespace ApplicationManagement
{
    /// <summary>
    /// Static class <see cref="MiniDump"/>
    /// </summary>
    public static class MiniDump
    {
        #region MiniDumpToFile
        /// <summary>
        /// Writes user-mode minidump information to the specified file.
        /// </summary>
        /// <param name="fileToDump">A string to the file in which the information is to be written.</param>
        /// <param name="dumpType">The type of information to be generated. 
        /// This parameter can be one or more of the values from the MINIDUMP_TYPE enumeration.</param>
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
        public static bool MiniDumpToFile(string fileToDump, Dbghelp.MINIDUMP_TYPE dumpType)
        {
            if(Environment.OSVersion.Platform != PlatformID.Win32NT)
                return false;
            bool result;
            using (FileStream fsToDump = File.Exists(fileToDump) ? File.Open(fileToDump, FileMode.Append) : File.Create(fileToDump))
            {
                Process thisProcess = Process.GetCurrentProcess();
                result = Dbghelp.MiniDumpWriteDump(thisProcess.Handle, thisProcess.Id,
                                          fsToDump.SafeFileHandle.DangerousGetHandle(), dumpType,
                                          IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
                fsToDump.Close();
            }
            return result;
        }

        /// <summary>
        /// Writes user-mode minidump information to the specified file.
        /// </summary>
        /// <param name="fileToDump">A string to the file in which the information is to be written.</param>
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
        public static bool MiniDumpToFile(string fileToDump)
        {
            return MiniDumpToFile(fileToDump, Dbghelp.MINIDUMP_TYPE.MiniDumpNormal);
        }
        #endregion
    }
}