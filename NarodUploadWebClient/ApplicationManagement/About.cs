namespace ApplicationManagement
{
    /// <summary>
    /// Provide information about the library.
    /// </summary>
    public static class About
    {
        #region Constants
        /// <summary>
        /// Library Author.
        /// </summary>
        public const string ProjectAuthor = "Tiago Conceição";

        /// <summary>
        /// Library HomePages.
        /// </summary>
        public static string[] ProjectWebpage = new string[] { 
            "http://www.codeproject.com/KB/miscctrl/ApplicationManagement.aspx", 
            "http://appmanagement.codeplex.com/",
            "https://sourceforge.net/projects/apmanagement/"
        };

        /// <summary>
        /// Library Donations.
        /// </summary>
        public const string ProjectDonations = "https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=CKQZDNUV2ABVU";

        /// <summary>
        /// Library Donations via SourceForge.
        /// </summary>
        public const string ProjectSFDonations = "http://sourceforge.net/donate/index.php?group_id=305202";

        /// <summary>
        /// Gets a value indicating if project has been compiled with MONO
        /// </summary>
#if MONO
        public const bool ProjectMonoCompiled = true;
#else 
        public const bool ProjectMonoCompiled = false;
#endif
        //public const double ProjectNETFramework = 2.0;

        #endregion
    }
}