using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SCServer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new SimConnectServer());
            } catch (Exception ex) {
                MessageBox.Show("Error - Likely caused by missing simconnect library. Install from the SimToolkitPro settings.\n\n" +ex.Message );
                Application.Exit();
            }
        }
    }
}
