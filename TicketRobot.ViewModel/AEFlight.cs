using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketRobot.ViewModel
{
    public class AERequestViewModel
    {
        public string region { get; set; }
        public string in_d_city { get; set; }
        public string in_a_city { get; set; }
        public string departureMonth { get; set; }
        public string departureDay { get; set; }
        public string returnMonth { get; set; }
        public string returnDay { get; set; }
        public string in_fit_o { get; set; }
        public string in_stops { get; set; }
        public string in_dptarv { get; set; }
        public string currentCalForm { get; set; }
        public string currentdate { get; set; }
    }
    public class AEFlightViewModel : IFlight
    {

        public string fdate
        {
            get;
            set;
        }

        public string tdate
        {
            get;
            set;
        }

        public string flightNum
        {
            get;
            set;
        }

        public string fAirport
        {
            get;
            set;
        }

        public string tAirport
        {
            get;
            set;
        }

        public string carr
        {
            get;
            set;
        }

        public string infomation
        {
            get;
            set;
        }


        public string @class
        {
            get;
            set;
        }

        public int stopNum
        {
            get;
            set;
        }

        public string equipment
        {
            get;
            set;
        }

        public string flyTime
        {
            get;
            set;
        }
    }
}
