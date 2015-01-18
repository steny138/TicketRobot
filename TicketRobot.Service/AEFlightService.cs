using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketRobot.ViewModel;

namespace TicketRobot.Service
{
    public class AEFlightService
    {
        public string setPostBody(AERequestViewModel request)
        {
            string result = string.Empty;
            result += "region=" + request.region;
            result += "&in_d_city=" + request.in_d_city;
            result += "&in_a_city=" + request.in_a_city;
            result += "&departureMonth=" + request.departureMonth;
            result += "&departureDay=" + request.departureDay;
            result += "&returnMonth=" + request.returnMonth;
            result += "&returnDay=" + request.returnDay;
            result += "&in_fit_o=" + request.in_fit_o;
            result += "&in_stops=" + request.in_stops;
            result += "&in_dptarv=" + request.in_dptarv;
            result += "&currentCalForm=" + request.currentCalForm;
            result += "&currentdate=" + request.currentdate;
            return result;
        }
        public List<AEFlightViewModel> parse(HtmlDocument doc)
        {
            List<AEFlightViewModel> result = new List<AEFlightViewModel>();
            var collection = doc.DocumentNode.SelectNodes("//*[@id=\"content\"]/table/tr/td[2]/div/table[4]/tr");

            foreach (HtmlNode node in collection)
            {
                if ((node.Attributes.Contains("class") && node.Attributes["class"].Value == "blue") ||
                    (node.SelectSingleNode("td").Attributes.Contains("colspan") && node.SelectSingleNode("td").Attributes["colspan"].Value == "16"))
                {
                    continue;
                }

                if (node.SelectNodes("td[@class='ct']") != null && node.SelectNodes("td[@class='ct']").Count == 9)
                {
                    var tdcollection = node.SelectNodes("td[@class='ct']");
                    AEFlightViewModel aeViewModel = new AEFlightViewModel();
                    //確認這班飛機有沒有飛
                    var chkFlight = node.SelectSingleNode("th[@class='scheFlightAvl'][div/img]");
                    if(chkFlight == null)
                    {
                        continue;
                    }
                    aeViewModel.flightNum = tdcollection[0].SelectSingleNode("div").InnerText;
                    
                }
            }

            return result;


        }
    }
}
