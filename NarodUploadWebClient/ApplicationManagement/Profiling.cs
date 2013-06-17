using System;
using System.Collections.Generic;
[assembly: CLSCompliant(true)]
namespace ApplicationManagement
{
    /// <summary>
    /// Initializes a new instance of the <see cref="T:ApplicationManagement.Profiling"/> class. 
    /// </summary>
    public sealed class Profiling : IDisposable
    {
        #region ProfilingData Class
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ApplicationManagement.Profiling.ProfilingData"/> class. 
        /// </summary>
        public sealed class ProfilingData
        {
            #region Properties
            /// <summary>
            /// Gets a <see cref="DateTime"/> Object containing the Date and Time when the profiling was started. 
            /// Note: If the value is equal to 'DateTine.MinValue' means the profiling never got started.
            /// </summary>
            private DateTime _startDate;

            /// <summary>
            /// Gets a <see cref="DateTime"/> Object containing the Date and Time when the profiling was started. 
            /// Note: If the value is equal to 'DateTine.MinValue' means the profiling never got started.
            /// </summary>
            public DateTime StartDate
            {
                get { return _startDate; }
            }

            /// <summary>
            /// Gets a <see cref="DateTime"/> Object containing the Date and Time when the profiling was ended. 
            /// Note: If the value is equal to 'DateTine.MinValue' means the profiling not end.
            /// </summary>
            private DateTime _endDate;

            /// <summary>
            /// Gets a <see cref="DateTime"/> Object containing the Date and Time when the profiling was ended. 
            /// Note: If the value is equal to 'DateTine.MinValue' means the profiling not end.
            /// </summary>
            public DateTime EndDate
            {
                get { return _endDate; }
            }

            /// <summary>
            /// Gets a <see cref="TimeSpan"/> Object containing the total time taken of profiling function. 
            /// Note: If the value is equal to 'TimeSpan.Zero' means the profiling is not concluded.
            /// </summary>
            private TimeSpan _timeTaken;

            /// <summary>
            /// Gets a <see cref="TimeSpan"/> Object containing the total time taken of profiling function. 
            /// Note: If the value is equal to 'TimeSpan.Zero' means the profiling is not concluded.
            /// </summary>
            public TimeSpan TimeTaken
            {
                get { return _timeTaken; }
            }

            /// <summary>
            /// Gets a value indicating if the profiling function had been concluded, this.Stop();
            /// </summary>
            public bool IsConcluded
            {
                get { return !_timeTaken.Equals(TimeSpan.Zero); }
            }

            #endregion

            #region Constructors
            /// <summary>
            /// Initializes a new instance of the <see cref="T:ApplicationManagement.Profiling.ProfilingData"/> class.
            /// </summary>
            public ProfilingData()
            {
                Reset();
            }
            #endregion 

            #region Public Methods
            /// <summary>
            /// Starts profiling.
            /// </summary>
            /// <returns>Returns true if Starts normaly, otherwise false.</returns>
            public bool Start()
            {
                if(_startDate != DateTime.MinValue)
                    return false;
                _startDate = DateTime.Now;
                return true;
            }

            /// <summary>
            /// Stops profiling.
            /// </summary>
            /// <returns>Returns true if Stops normaly, otherwise false.</returns>
            public bool Stop()
            {
                if (_startDate == DateTime.MinValue)
                    return false;
                _endDate = DateTime.Now;
                _timeTaken = _endDate - _startDate;
                return true;
            }


            /// <summary>
            /// Stop any current profiling and reset class to reuse. 
            /// </summary>
            public void Reset()
            {
                _startDate = DateTime.MinValue;
                _endDate = DateTime.MinValue;
                _timeTaken = TimeSpan.Zero;
            }
            #endregion 
        }
        #endregion

        #region Properties
        /// <summary>
        /// Store a <see cref="Dictionary{TKey,TValue}"/> collection of profiling functions, <see cref="ProfilingData"/>
        /// </summary>
        private Dictionary<string, ProfilingData> _profiles;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes the class <see cref="Profiling"/>
        /// </summary>
        public Profiling()
        {
            _profiles = new Dictionary<string, ProfilingData>();
        }

        /// <summary>
        /// Initializes the class, and create a profiling.
        /// <param name="name">A string that contains a unique name.</param>
        /// <param name="start">If true create and starts the profiling if possible, otherwise create only.</param>
        /// </summary>
        public Profiling(string name, bool start) : this()
        {
            Create(name, start);
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new profiling function.
        /// </summary>
        /// <param name="name">A string that contains a unique name.</param>
        /// <param name="start">If true create and starts the profiling if possible, otherwise create only.</param>
        /// <returns>Returns a <see cref="ProfilingData"/> Class for handle a profiling directly, 
        /// returns null if <paramref name="name"/> already exist.</returns>
        public ProfilingData Create(string name, bool start)
        {
            if (_profiles.ContainsKey(name))
                return null;
            ProfilingData pd = new ProfilingData();
            _profiles.Add(name, pd);
            if (start)
                pd.Start();
            return pd;
        }

        /// <summary>
        /// Creates a new profiling function.
        /// </summary>
        /// <param name="name">A string that contains a unique name.</param>
        /// <returns>Returns a <see cref="ProfilingData"/> Class for handle a profiling directly, 
        /// returns null if <paramref name="name"/> already exist.</returns>
        public ProfilingData Create(string name)
        {
            return Create(name, false);
        }

        /// <summary>
        /// Remove a profiling function.
        /// </summary>
        /// <param name="name">A string that contains a unique name.</param>
        /// <returns>Returns true if the profiling name got removed successful, otherwise false.</returns>
        /// <remarks>Stop(); before Remove(); is unless.</remarks>
        public bool Remove(string name)
        {
            return _profiles.ContainsKey(name) && _profiles.Remove(name);
        }

        /// <summary>
        /// Starts a profiling function.
        /// </summary>
        /// <param name="name">A string that contains a unique name.</param>
        /// <returns>Returns true if the profiling starts successful, otherwise false.</returns>
        public bool Start(string name)
        {
            return _profiles.ContainsKey(name) && _profiles[name].Start();
        }

        /// <summary>
        /// Stop a profiling function.
        /// </summary>
        /// <param name="name">A string that contains a unique name.</param>
        /// <returns>Returns true if the profiling stops successful, otherwise false.</returns>
        public bool Stop(string name)
        {
            return _profiles.ContainsKey(name) && _profiles[name].Stop();
        }

        /// <summary>
        /// Resets a profiling function.
        /// </summary>
        /// <param name="name">A string that contains a unique name.</param>
        /// <returns>Returns true if the profiling resets successful, otherwise false.</returns>
        public bool Reset(string name)
        {
            if (!_profiles.ContainsKey(name))
                return false;
            _profiles[name].Reset();
            return true;
        }

        /// <summary>
        /// Retrieve a <see cref="ProfilingData"/> from collection, for handle a profiling directly.
        /// </summary>
        /// <param name="name">A string that contains a unique name.</param>
        /// <returns>Returns a <see cref="ProfilingData"/> class, if <paramref name="name"/> not exists returns null.</returns>
        public ProfilingData Get(string name)
        {
            return !_profiles.ContainsKey(name) ? null : _profiles[name];
        }

        /// <summary>
        /// Check for a profiling existence.
        /// </summary>
        /// <param name="name">A string that contains a unique name.</param>
        /// <returns>Returns true if the profiling name exists, otherwise false.</returns>
        public bool Exists(string name)
        {
            return _profiles.ContainsKey(name);
        }
        #endregion

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            _profiles.Clear();
            _profiles = null;
        }

        #endregion
    }
}