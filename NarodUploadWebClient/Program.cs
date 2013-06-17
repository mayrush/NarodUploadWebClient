using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using ApplicationManagement;

namespace NarodUploadWebClient
{
    static class Program
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        #region Variables

        //public static Instance instance;
        public static MemoryManagement memory;
        public static Profiling profiling;
        public static ExceptionHandler exceptionHandler;

        #endregion Variables

        private const int SW_RESTORE = 9;

        public static string[] Filename = null;

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            #region Profiling Class

            // Our profiling will determinate the time taken by the app to initialize.
            //profiling = new Profiling();
            //Profiling.ProfilingData profilingData = profiling.Create("InitApp", true);

            #endregion Profiling Class

            #region Instance Class (Make single instance app)

            // Check if that is a single instance
            //instance = new Instance(Application.ProductName);
            // instance is created but make sure you call next line:
            //if (!instance.MakeSingleInstance(true, false))
            //{
            //    Instance.ShowDuplicateInstanceDialogError();
            // Stop and quit App.
            //    return;
            //}

            #endregion Instance Class (Make single instance app)

            #region Memory Management class

            // MemoryManagement only make effect in WinNT Systems,
            // if your app is multi system use that.
            if (MemoryManagement.CanUseClass())
            {
                memory = new MemoryManagement();
                memory.Start();
            }
            else
            {
                memory = null;
            }

            #endregion Memory Management class

            #region Exception Handler Class (Automatic cath and log unhandled exceptions)

            // Automatic cath and log unhandled exceptions
            exceptionHandler = new ExceptionHandler
                                   {
                                       PrefixText = "В этом файле содержатся ошибки программы",
                                       SuffixText = "Пожалуйста, отправьте их по e-mail: killoe@mail.ru"
                                   };
            // Next two lines are optional
            exceptionHandler.StartHandlingExceptions();

            #endregion Exception Handler Class (Automatic cath and log unhandled exceptions)

            #region End started profile and show results

            //profilingData.Stop();
            //MessageBox.Show(
            //string.Format("Start Date: {0}\nEnd Date: {1}\nTime Taken: {2}ms", profilingData.StartDate, profilingData.EndDate, profilingData.TimeTaken.TotalMilliseconds), "Our application initializes time");

            #endregion End started profile and show results

            bool createdNew = true;
            using (var mutex = new Mutex(true, "Loveworthy.Narod.Disk", out createdNew))
            {
                if ((createdNew) && (args.Length == 0))
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainForm());
                }
                else
                {
                    if (args.Length == 0)
                    {
                        var current = Process.GetCurrentProcess();
                        foreach (
                            var process in
                                Process.GetProcessesByName(current.ProcessName).Where(
                                    process => process.Id != current.Id))
                        {
                            SetForegroundWindow(process.MainWindowHandle);
                            ShowWindow(process.MainWindowHandle, SW_RESTORE);
                            break;
                        }
                    }
                    else
                    {
                        var AppSettings = new NuSettings("", "");

                        var SettingsLoaded = false;
                        if (File.Exists(Application.StartupPath + @"\Settings.xml"))
                        {
                            NuSettings.LoadSettings(AppSettings, Application.StartupPath);
                            SettingsLoaded = true;
                        }
                        else if (File.Exists(Application.StartupPath + @"\SavedSettings.bin"))
                        {
                            Stream appFileStream = File.OpenRead(Application.StartupPath + @"\SavedSettings.bin");
                            var deserializer = new BinaryFormatter();
                            AppSettings = (NuSettings)deserializer.Deserialize(appFileStream);
                            appFileStream.Close();

                            SettingsLoaded = true;
                        }

                        if (SettingsLoaded)
                        {
                            var tPass = AppSettings.Password;
                            var crPass = "";
                            if (tPass != "") crPass = Crypt.Decrypt(tPass, "XWERD");

                            AppSettings.ShowInTaskBar = true;
                            if (AppSettings.WndOpacity < 25)
                            {
                                AppSettings.WndOpacity = 75;
                            }

                            if ((crPass == "") || (AppSettings.Login == ""))
                            {
                                Application.Exit();
                            }
                            else
                            {
                                FilesProcess.AppSettings = AppSettings;

                                Application.EnableVisualStyles();
                                Application.Run(new UploadArgsForm(AppSettings.Login, crPass, "", args));
                                Application.Exit();
                            }
                        }
                    }
                }
            }
        }
    }
}