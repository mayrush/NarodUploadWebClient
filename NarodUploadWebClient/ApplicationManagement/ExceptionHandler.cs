using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace ApplicationManagement
{
    /// <summary>
    /// Cath, handle and log unhandled exceptions.
    /// </summary>
    public sealed class ExceptionHandler
    {
        #region Properties
        /// <summary>
        /// Gets the default path (Recommended) to Log and Dump path. 
        /// 
        /// Application.StartupPath + "\Logs\AppErrors"
        /// </summary>
        public static string DefaultPath
        {
            get { return string.Format("{0}{1}Logs{1}AppErrors", Application.StartupPath, Path.DirectorySeparatorChar); }
        }

        /// <summary>
        /// Gets or Sets the application name to show on logs and events.
        /// </summary>
        private string _appName;
        /// <summary>
        /// /// <summary>
        /// Gets or Sets the application name to show on logs and events.
        /// </summary>
        /// </summary>
        public string AppName
        {
            get { return _appName; }
            set { _appName = value; }
        }

        /// <summary>
        /// Gets or Sets the full path to store log files.
        /// </summary>
        private string _logPath;
        /// <summary>
        /// Gets or Sets the full path to store log files.
        /// </summary>
        public string LogPath
        {
            get { return _logPath; }
            set { _logPath = value;}
        }

        /// <summary>
        /// Gets or Sets the full path to store dump files.
        /// </summary>
        private string _dumpPath;
        /// <summary>
        /// Gets or Sets the full path to store dump files.
        /// </summary>
        public string DumpPath
        {
            get { return _dumpPath; }
            set { _dumpPath = value; }
        }

        /// <summary>
        /// Gets or Sets if application can generate a log file for exception.
        /// </summary>
        private bool _useLog;
        /// <summary>
        /// Gets or Sets if application can generate a log file for exception.
        /// </summary>
        public bool UseLog
        {
            get { return _useLog; }
            set { _useLog = value; }
        }

        /// <summary>
        /// Gets or Sets if application can generate a dump file for exception.
        /// </summary>
        private bool _useDump;
        /// <summary>
        /// Gets or Sets if application can generate a dump file for exception.
        /// </summary>
        public bool UseDump
        {
            get { return _useDump; }
            set { _useDump = value; }
        }

        /// <summary>
        /// Sets the prefix text on log files.
        /// </summary>
        private string _prefixText;

        /// <summary>
        /// Sets or Gets the prefix text on log files.
        /// </summary>
        public string PrefixText
        {
            get { return _prefixText; }
            set { _prefixText = value; }
        }

        /// <summary>
        /// Sets the suffix text on log files.
        /// </summary>
        private string _suffixText;

        /// <summary>
        /// Sets or Gets the suffix text on log files.
        /// </summary>
        public string SuffixText
        {
            get { return _suffixText; }
            set { _suffixText = value; }
        }

        /// <summary>
        /// Gets if application is automatic handling exceptions. 
        /// Note: Use StartHandlingExceptions(); or StopHandlingExceptions();
        /// </summary>
        private bool _isHandlingExceptions;
        /// <summary>
        /// Gets if application is automatic handling exceptions. 
        /// Note: Use StartHandlingExceptions(); or StopHandlingExceptions();
        /// </summary>
        public bool IsHandlingExceptions
        {
            get { return _isHandlingExceptions; }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandler"/> class. 
        /// </summary>
        /// <param name="appName">Application name to show on logs, titles, etc.</param>
        /// <param name="logPath">The path for store TXT logs, null to use the default path.</param>
        /// <param name="dumpPath">The path for store dump files, null to use the default path.</param>
        /// <param name="useLog">If true will log all errors to a simple text file.</param>
        /// <param name="useDump">If true will dump all errors to a file, to be debug latter.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ExceptionHandler(string appName, string logPath, string dumpPath, bool useLog, bool useDump)
        {
            if (string.IsNullOrEmpty(appName))
                throw new ArgumentNullException(appName);
            _prefixText = string.Empty;
            _suffixText = string.Empty;
            _appName = appName;
            _logPath = string.IsNullOrEmpty(logPath) ? DefaultPath : logPath;
            _dumpPath = string.IsNullOrEmpty(dumpPath) ? DefaultPath : dumpPath;
            _useLog = useLog;
            _useDump = useDump;
            _isHandlingExceptions = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandler"/> class. 
        /// Note: Application.ProductName will be used as AppName.
        /// </summary>
        /// <param name="logPath">The path for store TXT logs, null to use the default path.</param>
        /// <param name="dumpPath">The path for store dump files, null to use the default path.</param>
        /// <param name="useLog">If true will log all errors to a simple text file.</param>
        /// <param name="useDump">If true will dump all errors to a file, to be debug latter.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ExceptionHandler(string logPath, string dumpPath, bool useLog, bool useDump)
            : this(Application.ProductName, logPath, dumpPath, useLog, useDump)
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandler"/> class. 
        /// </summary>
        /// <param name="path">The path for store both txt and dump files.</param>
        /// <param name="useLog">If true will log all errors to a simple text file.</param>
        /// <param name="useDump">If true will dump all errors to a file, to be debug latter.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ExceptionHandler(string path, bool useLog, bool useDump) : this(Application.ProductName, path, path, useLog, useDump)
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandler"/> class.
        /// Note: This Constructor will use Application.ProductName, active both log methods.
        /// </summary>
        /// <param name="path">The path for store both txt and dump files.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ExceptionHandler(string path)
            : this(Application.ProductName, path, path, true, true)
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandler"/> class. 
        /// Note: This Constructor will use Application.ProductName, DefaultPath and active both log methods.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public ExceptionHandler()
            : this(Application.ProductName, DefaultPath, DefaultPath, true, true)
        {

        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Start handling automatic the exceptions. 
        /// </summary>
        public void StartHandlingExceptions()
        {
            if(_isHandlingExceptions)
                return;
            Application.ThreadException += Application_ThreadException;
            try
            {
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            }
            catch {}
            
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            _isHandlingExceptions = true;
        }

        /// <summary>
        /// Stop handling automatic the exceptions. 
        /// </summary>
        public void StopHandlingExceptions()
        {
            if (!_isHandlingExceptions)
                return;
            Application.ThreadException -= Application_ThreadException;
            //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.Automatic);
            AppDomain.CurrentDomain.UnhandledException -= CurrentDomain_UnhandledException;
            _isHandlingExceptions = false;
        }

        /// <summary>
        /// Clear all logs including folders.
        /// </summary>
        public void ClearAllLogs()
        {
            try
            {
                if (Directory.Exists(_logPath))
                {
                    string[] files = Directory.GetFiles(_logPath, "*.txt", SearchOption.TopDirectoryOnly);
                    for (int i = 0; i < files.Length; i++)
                    {
                        File.Delete(files[i]);
                    }
                    Directory.Delete(_logPath);
                }
            }
            catch {}


            try
            {
                if (Directory.Exists(_dumpPath))
                {
                    string[] files = Directory.GetFiles(_dumpPath, "*.dmp", SearchOption.TopDirectoryOnly);
                    for (int i = 0; i < files.Length; i++)
                    {
                        File.Delete(files[i]);
                    }
                    Directory.Delete(_dumpPath);
                }
            }
            catch { }
        }

        #endregion

        #region WriteCrashToFile

        /// <summary>
        /// Writes a crash to file, including dump creation.
        /// </summary>
        /// <param name="ex">Exception value.</param>
        private void WriteCrashToFile(Exception ex)
        {
            if (_useLog)
            {
                if (!Directory.Exists(_logPath))
                    Directory.CreateDirectory(_logPath);
                string filenameTxt = string.Format("{0}__{1}.txt", Path.Combine(_logPath, _appName),
                                                   DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss"));
                using (TextWriter logtoFile = new StreamWriter(filenameTxt))
                {
                    logtoFile.WriteLine("[{0}]", _appName);
                    logtoFile.WriteLine("Date={0}", DateTime.Now);
                    logtoFile.WriteLine("Application={0} {1}", Application.ProductName, Application.ProductVersion);
                    logtoFile.WriteLine("ExecutablePath={0}", Application.ExecutablePath);
                    logtoFile.WriteLine("Company={0}", Application.CompanyName);
                    logtoFile.WriteLine();
                    if (!string.IsNullOrEmpty(_prefixText))
                    {
                        logtoFile.WriteLine(_prefixText);
                        logtoFile.WriteLine();
                    }
                    logtoFile.WriteLine("########################");
                    logtoFile.WriteLine("##        Error       ##");
                    logtoFile.WriteLine("########################");
                    //logtoFile.WriteLine("[Error]");
                    int count = 1;
                    Exception exception = ex;
                    while (exception != null)
                    {
                        logtoFile.WriteLine("[Exception{0}]", count);
                        logtoFile.WriteLine("Message={0}", exception.Message);
                        //StackTrace stackTrace = new StackTrace(exception);
                        /*for (int x = 0; x < stackTrace.FrameCount; x++)
                        {
                            StackFrame stackFrame = stackTrace.GetFrame(x);
                            MethodBase methodBase = stackFrame.GetMethod();
                            logtoFile.WriteLine("StackFrame{0}={1}", x + 1, stackFrame);
                            logtoFile.WriteLine("MethodBase{0}={1}", x + 1, methodBase);
                        }*/
                        foreach (DictionaryEntry data in exception.Data)
                        {
                            logtoFile.WriteLine("{0}={1}", data.Key, data.Value);
                        }
                        logtoFile.WriteLine("StackTrace={0}", exception.StackTrace);
                        logtoFile.WriteLine("TargetSite={0}", exception.TargetSite);
                        logtoFile.WriteLine("Source={0}", exception.Source);
                        logtoFile.WriteLine("HelpLink={0}", exception.HelpLink);
                        count++;
                        exception = exception.InnerException;
                        logtoFile.WriteLine();
                    }
                    logtoFile.WriteLine();
                    logtoFile.WriteLine();
                    logtoFile.WriteLine("########################");
                    logtoFile.WriteLine("## System Information ##");
                    logtoFile.WriteLine("########################");
                    logtoFile.WriteLine("[System]");
                    logtoFile.WriteLine("OperativeSystem={0}", SystemInformationEx.GetOSName(true, true));
                    logtoFile.WriteLine("ProcessorCount={0}", SystemInformationEx.GetProcessorCount());
                    logtoFile.WriteLine();
                    logtoFile.WriteLine();
                    logtoFile.WriteLine("########################");
                    logtoFile.WriteLine("##     Open Forms     ##");
                    logtoFile.WriteLine("########################");
                    logtoFile.WriteLine("[Forms]");
                    for (int i = 0; i < Application.OpenForms.Count; i++)
                    {
                        //Type frmtype = typeof(Application.OpenForms[i]);
                        logtoFile.WriteLine("Form{0}={1}", i + 1, Application.OpenForms[i]);
                    }
                    logtoFile.WriteLine();
                    logtoFile.WriteLine();
                    logtoFile.WriteLine("########################");
                    logtoFile.WriteLine("## Loaded Assemblies  ##");
                    logtoFile.WriteLine("########################");
                    /*logtoFile.Write("[{1}]{0}Date={3}{0}{2}{0}{0}{4}{0}{0}########################{0}## System Information: ##{0}########################{0}{0}{0}########################{0}## Loaded Assemblies: ##{0}########################{0}", 
                        Environment.NewLine, 
                        _appName,
                        _prefixText, 
                        DateTime.Now, 
                        text);*/
                    Assembly[] assemblies = AssemblyEx.GetLoadedAssemblies();
                    for (int i = 0; i < assemblies.Length; i++)
                    {
                        logtoFile.WriteLine("[{1}]{0}   Location={2}{0}", Environment.NewLine, assemblies[i].FullName, assemblies[i].Location);
                    }
                    if (!string.IsNullOrEmpty(_suffixText))
                        logtoFile.WriteLine(_suffixText);
                    logtoFile.Close();
                }
            }
            if (_useDump && Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                if (!Directory.Exists(_dumpPath))
                    Directory.CreateDirectory(_dumpPath);
                MiniDump.MiniDumpToFile(string.Format("{0}__{1}.dmp", Path.Combine(_dumpPath, _appName),
                                                      DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")));
            }
            //return string.Format("{0}{1}", AppCrashLogs, filename);
        }

        private void WriteCrashToFile(string text)
        {
            if (_useLog)
            {
                if (!Directory.Exists(_logPath))
                Directory.CreateDirectory(_logPath);
                string filenameTxt = string.Format("{0}__{1}.txt", Path.Combine(_logPath, _appName),
                                                   DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss"));
                using (TextWriter logtoFile = new StreamWriter(filenameTxt))
                {
                    logtoFile.WriteLine("[{0}]", _appName);
                    logtoFile.WriteLine("Date={0}", DateTime.Now);
                    logtoFile.WriteLine("Application={0} {1}", Application.ProductName, Application.ProductVersion);
                    logtoFile.WriteLine("ExecutablePath={0}", Application.ExecutablePath);
                    logtoFile.WriteLine("Company={0}", Application.CompanyName);
                    logtoFile.WriteLine();
                    if (!string.IsNullOrEmpty(_prefixText))
                    {
                        logtoFile.WriteLine(_prefixText);
                        logtoFile.WriteLine();
                    }
                    logtoFile.WriteLine("########################");
                    logtoFile.WriteLine("##        Error       ##");
                    logtoFile.WriteLine("########################");
                    logtoFile.WriteLine("[Error]");
                    logtoFile.WriteLine(text);
                    logtoFile.WriteLine();
                    logtoFile.WriteLine();
                    logtoFile.WriteLine("########################");
                    logtoFile.WriteLine("## System Information ##");
                    logtoFile.WriteLine("########################");
                    logtoFile.WriteLine("[System]");
                    logtoFile.WriteLine("OperativeSystem={0}", SystemInformationEx.GetOSName(true, true));
                    logtoFile.WriteLine("ProcessorCount={0}", SystemInformationEx.GetProcessorCount());
                    logtoFile.WriteLine();
                    logtoFile.WriteLine();
                    logtoFile.WriteLine("########################");
                    logtoFile.WriteLine("##     Open Forms     ##");
                    logtoFile.WriteLine("########################");
                    logtoFile.WriteLine("[Forms]");
                    for(int i = 0; i < Application.OpenForms.Count; i++)
                    {
                        //Type frmtype = typeof(Application.OpenForms[i]);
                        logtoFile.WriteLine("Form{0}={1}", i + 1, Application.OpenForms[i]);    
                    }
                    logtoFile.WriteLine();
                    logtoFile.WriteLine();
                    logtoFile.WriteLine("########################");
                    logtoFile.WriteLine("## Loaded Assemblies  ##");
                    logtoFile.WriteLine("########################");
                    /*logtoFile.Write("[{1}]{0}Date={3}{0}{2}{0}{0}{4}{0}{0}########################{0}## System Information: ##{0}########################{0}{0}{0}########################{0}## Loaded Assemblies: ##{0}########################{0}", 
                        Environment.NewLine, 
                        _appName,
                        _prefixText, 
                        DateTime.Now, 
                        text);*/
                    Assembly[] assemblies = AssemblyEx.GetLoadedAssemblies();
                    for (int i = 0; i < assemblies.Length; i++)
                    {
                        logtoFile.WriteLine("[{1}]{0}   Location={2}{0}", Environment.NewLine, assemblies[i].FullName, assemblies[i].Location);
                    }
                    if (!string.IsNullOrEmpty(_suffixText))
                        logtoFile.WriteLine(_suffixText);
                    logtoFile.Close();
                }
            }
            if (_useDump && Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                if (!Directory.Exists(_dumpPath))
                Directory.CreateDirectory(_dumpPath);
                MiniDump.MiniDumpToFile(string.Format("{0}__{1}.dmp", Path.Combine(_dumpPath, _appName),
                                                      DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")));
            }
            //return string.Format("{0}{1}", AppCrashLogs, filename);
        }

        #endregion

        #region ShowExceptionDialog

        /// <summary>
        /// Show a dialog with the exception information.
        /// </summary>
        /// <param name="title">Dialog title.</param>
        /// <param name="e">Exception to debug.</param>
        /// <returns>Returns the <see cref="DialogResult"/></returns>
        public static DialogResult ShowExceptionDialog(string title, Exception e)
        {
            if (string.IsNullOrEmpty(title))
                title = string.Format("{0} Error", Application.ProductName);
            string szErrorMsg = string.Format("An error occurred with the following information:\n\nMessage: {0}\nAt: {1}", e.Message, e.TargetSite);
            return MessageBox.Show(szErrorMsg, title, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
        }
        #endregion

        #region Public Exceptions Handler
        /// <summary>
        /// Handle Unhandled Exceptions. 
        /// For manualy use only! 
        /// Must be called from inside 'System.AppDomain.UnhandledException' Event}
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event Arguments <see cref="T:System.UnhandledExceptionEventArgs"/></param>
        public void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;
                // Since we can't prevent the app from terminating, log this to the event log.
                if (!EventLog.SourceExists(string.Format("{0} - UnhandledException", _appName)))
                {
                    EventLog.CreateEventSource(string.Format("{0} - UnhandledException", _appName), "Application");
                }

                // Create an EventLog instance and assign its source.
                EventLog myLog = new EventLog();
                myLog.Source = string.Format("{0} - ThreadException", _appName);
                myLog.WriteEntry(ex.ToString());
                WriteCrashToFile(ex);
            }
            catch (Exception exc)
            {
                try
                {
                    MessageBox.Show(string.Format("{0} Fatal Non-UI Error.\nCould not write the error to the event log.\nReason: {1}", _appName, exc.Message), string.Format("{0} Fatal Non-UI Error", _appName), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }
        }      

        /// <summary>
        /// Complete debug exception to a file.
        /// </summary>
        /// <param name="filename">Output file to save the information.</param>
        /// <param name="ex">Exception to debug.</param>
        /// <param name="tw"><see cref="TextWriter"/> Instance to write inner exception.</param>
        private static void DebugException(string filename, Exception ex, TextWriter tw)
        {
            StackTrace stackTrace = new StackTrace(ex);
            for (int i = 0; i < stackTrace.FrameCount; i++)
            {
                StackFrame stackFrame = stackTrace.GetFrame(i);
                /*MessageBox.Show(string.Format("File Column Number: {0}\nFile Line Number: {1}\nFile Name: {2}\nIL Offset: {3}\nNative Offset: {4}", 
                    stackFrame.GetFileColumnNumber(), 
                    stackFrame.GetFileLineNumber(),
                    stackFrame.GetFileName(),
                    stackFrame.GetILOffset(),
                    stackFrame.GetNativeOffset()
                    ));*/
                MethodBase methodBase = stackFrame.GetMethod();
                tw.WriteLine("[Frame{0}]", i);
                tw.WriteLine("FileColumnNumber={0}", stackFrame.GetFileColumnNumber());
                tw.WriteLine("GetFileLineNumber={0}", stackFrame.GetFileLineNumber());
                tw.WriteLine("GetFileName={0}", stackFrame.GetFileName());
                tw.WriteLine("GetILOffset={0}", stackFrame.GetILOffset());
                tw.WriteLine("GetNativeOffset={0}", stackFrame.GetNativeOffset());
                tw.WriteLine();
                tw.WriteLine("Attributes={0}", methodBase.Attributes);
                tw.WriteLine("CallingConvention={0}", methodBase.CallingConvention);
                tw.WriteLine("ContainsGenericParameters={0}", methodBase.ContainsGenericParameters);
                tw.WriteLine("DeclaringType={0}", methodBase.DeclaringType.Name);

                Type[] types = methodBase.GetGenericArguments();
                for (int x = 0; x < types.Length; x++)
                {
                    tw.WriteLine("GetGenericArguments{0}={1}", x, methodBase.GetGenericArguments()[0]);
                }
                tw.WriteLine("GetMethodBody={0}", methodBase.GetMethodBody());
                tw.WriteLine("GetMethodImplementationFlags={0}", methodBase.GetMethodImplementationFlags());
                ParameterInfo[] parameterInfo = methodBase.GetParameters();
                for(int x = 0; x<parameterInfo.Length; x++)
                {
                    tw.WriteLine("{0} {1}", parameterInfo[x].ParameterType.Name, parameterInfo[x].Name);
                }
                tw.WriteLine("GetType={0}", methodBase.GetType().Namespace);
                tw.WriteLine("IsAbstract={0}", methodBase.IsAbstract);
                tw.WriteLine("IsAssembly={0}", methodBase.IsAssembly);
                tw.WriteLine("IsConstructor={0}", methodBase.IsConstructor);
                tw.WriteLine("IsFamily={0}", methodBase.IsFamily);
                tw.WriteLine("IsFamilyAndAssembly={0}", methodBase.IsFamilyAndAssembly);
                tw.WriteLine("IsFamilyOrAssembly={0}", methodBase.IsFamilyOrAssembly);
                tw.WriteLine("IsFinal={0}", methodBase.IsFinal);
                tw.WriteLine("IsGenericMethod={0}", methodBase.IsGenericMethod);
                tw.WriteLine("IsGenericMethodDefinition={0}", methodBase.IsGenericMethodDefinition);
                tw.WriteLine("IsHideBySig={0}", methodBase.IsHideBySig);
                tw.WriteLine("IsPrivate={0}", methodBase.IsPrivate);
                tw.WriteLine("IsPublic={0}", methodBase.IsPublic);
                tw.WriteLine("IsSpecialName={0}", methodBase.IsSpecialName);
                tw.WriteLine("IsStatic={0}", methodBase.IsStatic);
                tw.WriteLine("IsVirtual={0}", methodBase.IsVirtual);
                tw.WriteLine("MemberType={0}", methodBase.MemberType);
                tw.WriteLine("MetadataToken={0}", methodBase.MetadataToken);
                tw.WriteLine("MethodHandle={0}", methodBase.MethodHandle);
                tw.WriteLine("Module={0}", methodBase.Module);
                tw.WriteLine("Name={0}", methodBase.Name);
                tw.WriteLine("ReflectedType={0}", methodBase.ReflectedType.Name);
                tw.WriteLine("methodBase.ToString={0}", methodBase);
                tw.WriteLine();
                /*ParameterInfo[] parameterInfo = methodBase.GetParameters();
                for(int x = 0; i<parameterInfo.Length; x++)
                {
                    //tw.WriteLine("={0}", ,parameterInfo[i]);
                }*/
                
                tw.WriteLine();
            }
            foreach (DictionaryEntry data in ex.Data)
            {
                MessageBox.Show(data.Key + "=" + data.Value);
            }
        }

        /// <summary>
        /// Complete debug exception to a file.
        /// </summary>
        /// <param name="filename">Output file to save the information.</param>
        /// <param name="ex">Exception to debug.</param>
        /// <param name="isBaseException">Use true if <paramref name="ex"/> is a base exception.</param>
        public static void DebugException(string filename, Exception ex, bool isBaseException)
        {
            using (TextWriter tw = new StreamWriter(filename))
            {
                if (isBaseException)
                {
                    Exception exInner = ex;
                    while (exInner != null)
                    {
                        DebugException(filename, exInner, tw);
                        exInner = exInner.InnerException;
                    }
                    return;
                }
                DebugException(filename, ex, tw);
                tw.Close();
            }
        }

        /// <summary>
        /// Handle Thread Exceptions. 
        /// For manualy use only! 
        /// Must be called from inside 'System.Windows.Forms.Application.ThreadException' Event}
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event Arguments <see cref="T:System.Threading.ThreadExceptionEventArgs"/></param>
        public void ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            DialogResult result = DialogResult.Cancel;
            try
            {
               // DebugException(e.Exception, true);
                
                //WriteCrashToFile(e.Exception.ToString()+Environment.NewLine+e.Exception.HelpLink);
                /*WriteCrashToFile(string.Format("Exception={6}{0}Message={1}{0}StackTrace={2}{0}TargetSite={3}{0}Source={4}{0}HelpLink={5}", 
                    Environment.NewLine, 
                    e.Exception.Message,
                    e.Exception.StackTrace,
                    e.Exception.TargetSite,
                    e.Exception.Source,
                    e.Exception.HelpLink,
                    e
                    ));*/
                WriteCrashToFile(e.Exception);
                result = ShowExceptionDialog(string.Format("{0} Error", _appName), e.Exception);
            }
            catch
            {
                try
                {
                    MessageBox.Show(string.Format("Fatal {0} Error", _appName), "Cannot detect the error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }

            // Exits the program when the user clicks Abort.
            if (result == DialogResult.Abort)
                Application.Exit();
        }
        #endregion

        #region Private Events Callbacks
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            UnhandledException(sender, e);
        }

        private void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ThreadException(sender, e);
        }
        #endregion
    }
}