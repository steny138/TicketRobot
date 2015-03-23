using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("出發時間")]
        public string fdate
        {
            get;
            set;
        }
        [DisplayName("到達時間")]
        public string tdate
        {
            get;
            set;
        }
        [DisplayName("航班號碼")]
        public string flightNum
        {
            get;
            set;
        }
        [DisplayName("出發地")]
        public string fAirport
        {
            get;
            set;
        }
        [DisplayName("目的地")]
        public string tAirport
        {
            get;
            set;
        }
        [DisplayName("航空公司")]
        public string carr
        {
            get;
            set;
        }
        [DisplayName("航班資訊")]
        public string infomation
        {
            get;
            set;
        }

        [DisplayName("艙等")]
        public string @class
        {
            get;
            set;
        }
        [DisplayName("停留點數量")]
        public int stopNum
        {
            get;
            set;
        }
        [DisplayName("機型")]
        public string equipment
        {
            get;
            set;
        }
        [DisplayName("飛行時間")]
        public string flyTime
        {
            get;
            set;
        }
    }
}
