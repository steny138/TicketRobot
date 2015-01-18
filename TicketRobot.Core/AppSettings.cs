using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketRobot.Core
{
    public class AppSettings
    {
        public static string AEUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings.Get("AE");
            }
        }
    }
}
