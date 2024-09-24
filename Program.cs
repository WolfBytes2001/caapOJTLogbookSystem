using caapOJTLogbookSystem.user_controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace caapOJTLogbookSystem
{
    internal static class Program
    {
        public static Dashboard dashboard = new Dashboard();
        public static LoginAuth1 loginAuth = new LoginAuth1();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
      
        static void Main()
        {
            Application.Run(loginAuth);
        }
    }
}
