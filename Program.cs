using System.Security.Cryptography.X509Certificates;

namespace FirstClicker
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        public static bool RestartForPrestige = false;
        [STAThread]
        public static void Main()
        {
            
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            do
            {
                Application.Run(new frmMain());
            }
            while (RestartForPrestige);
            
        }
    }
}