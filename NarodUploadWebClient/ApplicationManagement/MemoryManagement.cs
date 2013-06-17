using System;
using System.Diagnostics;
using System.Windows.Forms;
using ApplicationManagement.WINAPI;

namespace ApplicationManagement
{
    /// <summary>
    /// This class provide methods to automatic do the Memory Management, 
    /// free some resources and collect garbage
    /// </summary>
    public sealed class MemoryManagement : IDisposable
    {
        #region Properties
        /// <summary>
        /// Current app ticks
        /// </summary>
        private long _ticks;

        /// <summary>
        /// Gets a value indicating if the Memory Management is stopped or running.
        /// </summary>
        private bool _hasStarted;

        /// <summary>
        /// Gets a value indicating if the Memory Management is stopped or running.
        /// </summary>
        public bool HasStarted
        {
            get { return _hasStarted; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ApplicationManagement.MemoryManagement"/> class. 
        /// Note: This class is only for Win32NT Operative Systems, static methods still can be used in both.
        /// </summary>
        /// <param name="start">If true initializes the class and starts the memory management, 
        /// otherwise only initializes the class</param>
        /// <remarks>If that class is used in a unsupported Operative System, nothing will happen.</remarks>
        public MemoryManagement(bool start)
        {
            if (start)
                Start();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ApplicationManagement.MemoryManagement"/> class. 
        /// Note: This class is only for Win32NT Operative Systems, static methods still can be used in both.
        /// </summary>
        /// <remarks>If that class is used in a unsupported Operative System, nothing will happen.</remarks>
        public MemoryManagement()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Starts the automatic memory management.
        /// </summary>
        /// <returns>Returns true if function starts successful, otherwise false.</returns>
        public bool Start()
        {
            if (_hasStarted || Environment.OSVersion.Platform != PlatformID.Win32NT)
                return false;
            _ticks = DateTime.Now.Ticks;
            Application.Idle += Application_Idle;
            Free();
            _hasStarted = true;
            return true;
        }

        /// <summary>
        /// Stops the automatic memory management.
        /// </summary>
        /// <returns>Returns true if function stops successful, otherwise false.</returns>
        public bool Stop()
        {
            if (!_hasStarted)
                return false;
            Application.Idle -= Application_Idle;
            _ticks = 0;
            _hasStarted = false;
            return true;
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Free resources.
        /// </summary>
        private static void Free()
        {
            try
            {
                using (Process proc = Process.GetCurrentProcess())
                {
                    Kernel32.SetProcessWorkingSetSize(proc.Handle, -1, -1);
                }
            }
            catch (Exception)
            {
            }

        }

        /// <summary>
        /// Gets if this class <see cref="MemoryManagement"/> can be used. 
        /// </summary>
        /// <returns>Returns true if this class can be used under current Operative System, otherwise false.</returns>
        public static bool CanUseClass()
        {
            return Environment.OSVersion.Platform == PlatformID.Win32NT;
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Collect Garbage and free memory.
        /// </summary>
        public static void CollectGarbage()
        {
            GC.Collect(0);
            GC.GetTotalMemory(false);
            GC.Collect(0);
        }

        #endregion

        #region Event Methods

        /// <summary>
        /// When application is idle, this event is fired.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event aruments.</param>
        private void Application_Idle(object sender, EventArgs e)
        {
            try
            {
                long ticks = DateTime.Now.Ticks;
                if ((ticks - _ticks) > 0x989680L)
                {
                    _ticks = ticks;
                    Free();
                }
            }
            catch
            {
            }
        }

        #endregion

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            Stop();
        }

        #endregion
    }
}