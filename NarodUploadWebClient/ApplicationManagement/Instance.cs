using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace ApplicationManagement
{
    /// <summary>
    /// Control your application instance with the possibility of make your application single instance.
    /// </summary>
    public sealed class Instance : IDisposable
    {
        #region Properties
        /// <summary>
        /// A string that contain a unique instance name, that must be unique. 
        /// When some application finds a Mutex with same unique name will returns false. (Kill Application)
        /// </summary>
        private readonly string _instanceName;
        /// <summary>
        /// A string that contain a unique instance name, that must be unique. 
        /// When some application finds a Mutex with same unique name will returns false. (Kill Application)
        /// </summary>
        public string InstanceName
        {
            get { return _instanceName; }
        }

        /// <summary>
        /// Mutex that holds the instance name, 
        /// it can be null if function 'MakeSingleInstance' never got called.
        /// </summary>
        private Mutex _mutex;
        /// <summary>
        /// Mutex that holds the instance name, 
        /// it can be null if function 'MakeSingleInstance' never got called.
        /// </summary>
        public Mutex Mutex
        {
            get { return _mutex; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new class <see cref="ApplicationManagement.Instance"/>
        /// </summary>
        /// <param name="instanceName">A string that contain a unique instance name, that must be unique. 
        /// When some application finds a Mutex with same unique name will returns false. (Kill Application)
        /// </param>
        /// <exception cref="ArgumentNullException"></exception>
        public Instance(string instanceName)
        {
            if (string.IsNullOrEmpty(instanceName))
                throw new ArgumentNullException(instanceName);
            _instanceName = instanceName;
        }
        #endregion

        #region Public Methods

        #region MakeSingleInstance
        /// <summary>
        /// Make your application single instance only, that means user only can open one process of your application. 
        /// You must kill your application if this returns false.
        /// 
        /// NOTE: Call only one time, or you will get an ApplicationException
        /// </summary>
        /// <param name="global">Sets true if you need to limit your application to one instance across all sessions.</param>
        /// <param name="checkProcess">If true, it will check for a process with same name of the current process. 
        /// NOTE: That check is only performed if Mutex dont found a instance with same name.</param>
        /// <returns>Returns true if there are no other instances opened, otherwise false.</returns>
        /// <remarks>Use that function before run any form or any code that shouldn't be processed with more than 1 instance.</remarks>
        /// <exception cref="ApplicationException"></exception>
        public bool MakeSingleInstance(bool global, bool checkProcess)
        {
            if (_mutex != null)
                throw new ApplicationException(
                    "Function 'ApplicationManagement.Instance.MakeSingleInstance' can only be called one time.\nMutex is already created.");
            bool createdNew;
            _mutex = global ? new Mutex(true, string.Format("Global\\{0}Mutex", _instanceName), out createdNew) : new Mutex(true, string.Format("{0}Mutex", _instanceName), out createdNew);
            GC.KeepAlive(_mutex);
            if (!checkProcess)
                return createdNew;
            return PriorProcess() == null;
        }

        /// <summary>
        /// Make your application single instance only, that means user only can open one process of your application. 
        /// You must kill your application if this returns false.
        /// 
        /// NOTE: Call only one time, or you will get an ApplicationException
        /// </summary>
        /// <returns>Returns true if there are no other instances opened, otherwise false.</returns>
        /// <remarks>Use that function before run any form or any code that shouldn't be processed with more than 1 instance. 
        /// NOTE: That function make instance global and will not check for process equality.</remarks>
        /// <exception cref="ApplicationException"></exception>
        public bool MakeSingleInstance()
        {
            return MakeSingleInstance(true, false);
        }
        #endregion 

        #endregion

        #region Static Methods

        #region PriorProcess
        /// <summary>
        /// Returns a <see cref="T:System.Diagnostics.Process"/> pointing to
        /// a pre-existing process with the same name as the
        /// current one, if any; or null if the current process
        /// is unique.
        /// </summary>
        /// <returns>Returns a <see cref="T:System.Diagnostics.Process"/> pointing to
        /// a pre-existing process with the same name as the
        /// current one, if any; or null if the current process
        /// is unique.</returns>
        public static Process PriorProcess()
        {
            Process curr = Process.GetCurrentProcess();
            Process[] procs = Process.GetProcessesByName(curr.ProcessName);
            foreach (Process p in procs)
            {
                if (p.MainModule == null || curr.MainModule == null) continue;
                if ((p.Id == curr.Id) || (p.MainModule.FileName != curr.MainModule.FileName)) continue;
                curr.Dispose();
                return p;
            }
            return null;
        }
        #endregion

        #region ShowDuplicateInstanceDialogError

        /// <summary>
        /// Show a MessageBox saying "The application is already running." 
        /// Title: Application.ProductName 
        /// Buttons: Ok 
        /// Icon: Exclamation
        /// </summary>
        /// <returns>Returns a <see cref="DialogResult"/> with the button the user preset.</returns>
        public static DialogResult ShowDuplicateInstanceDialogError()
        {
            return MessageBox.Show("The application is already running.", Application.ProductName,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        #endregion

        #endregion

        #region Overrides of Object
        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return _instanceName;
        }
        #endregion

        #region Dispose
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources. 
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            if (_mutex == null) return;
            try
            {
                _mutex.Close();
                _mutex.ReleaseMutex();
            }
            catch
            {}
            
            _mutex = null;
        }
        #endregion
    }
}
