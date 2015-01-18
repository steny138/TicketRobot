using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketRobot.ViewModel
{
    interface IFlight
    {
        string fdate { get; set; }
        string tdate { get; set; }
        string flightNum { get; set; }
        string fAirport { get; set; }
        string tAirport { get; set; }
        string carr { get; set; }
        string infomation { get; set; }
        string @class { get; set; }
        int stopNum { get; set; }
        string equipment { get; set; }
        string flyTime { get; set; }

    }
}
