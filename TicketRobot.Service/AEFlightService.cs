using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketRobot.ViewModel;
using TicketRobot.Core;

namespace TicketRobot.Service
{
    public class AEFlightService
    {
        private string CARRIOR = "AE";
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
            int segmentCount = 0;
            if (collection == null) throw new Exception("查無資料");
            for(int i =0 ; i< collection.Count; i++)
            {
                HtmlNode node = collection[i];
                if ((node.Attributes.Contains("class") && node.Attributes["class"].Value == "blue") ||
                    (node.SelectSingleNode("td").Attributes.Contains("colspan") && node.SelectSingleNode("td").Attributes["colspan"].Value == "16"))
                {
                    continue;
                }

                if (node.SelectNodes("td[@class='ct']") != null && node.SelectNodes("td[@class='ct']").Count >5)
                {
                    try
                    {
                        segmentCount++;
                        var tdcollection = node.SelectNodes("td[@class='ct']");
                        var tdDetail = node.SelectSingleNode(string.Format("following-sibling::tr/td/div[@id='depFlightInfo{0}']/table/tr/td[@class='ct']/table/tr/td[@class='ct']/table", segmentCount));
                        var depNode = tdDetail.SelectSingleNode("tr[2]/td[@class='ct']/table/tr/td[2]/table/tr[2]");
                        var arrNode = tdDetail.SelectSingleNode("tr[2]/td[@class='ct']/table/tr/td[2]/table/tr[5]");
                        var tdInfo = tdDetail.SelectNodes("tr[3]/td[@class='ct']/table/tr");

                        AEFlightViewModel aeViewModel = new AEFlightViewModel();
                        //確認這班飛機有沒有飛
                        var chkFlight = node.SelectSingleNode("th[@class='scheFlightAvl'][div/img]");
                        if (chkFlight == null)
                        {
                            continue;
                        }
                        aeViewModel.flightNum = tdcollection[0].SelectSingleNode("div").InnerText;

                        aeViewModel.fdate = depNode.SelectSingleNode("td[@class='ct'][2]").InnerText
                            + " " + depNode.SelectNodes("td[@class='ct']").First().InnerText;

                        aeViewModel.tdate = arrNode.SelectSingleNode("td[@class='ct'][2]").InnerText
                            + " " + arrNode.SelectNodes("td[@class='ct']").First().InnerText;

                        aeViewModel.fAirport = depNode.SelectNodes("td[@class='ct']").Last().InnerText;
                        aeViewModel.tAirport = arrNode.SelectNodes("td[@class='ct']").Last().InnerText;
                        aeViewModel.carr = CARRIOR;
                        aeViewModel.infomation = tdInfo[0].SelectSingleNode("td[@class='ct'][2]/a").InnerText;
                        aeViewModel.flyTime = (tdInfo[1].SelectSingleNode("td[@class='ct'][2]").InnerText ?? string.Empty).Replace("&nbsp;", string.Empty);
                        aeViewModel.equipment = tdInfo[2].SelectSingleNode("td[@class='ct'][2]/a").InnerText;


                        aeViewModel.@class = tdInfo[3].SelectSingleNode("td[@class='ct'][2]/img").Attributes["src"].Value.Replace("imgs/", string.Empty).Replace(".gif", string.Empty);

                        aeViewModel.stopNum = int.Parse(tdInfo[4].SelectSingleNode("td[@class='ct'][2]/img").Attributes["src"].Value.Replace("imgs/bStop_", string.Empty).Replace(".gif", string.Empty));
                        result.Add(aeViewModel);
                        
                    }
                    catch (Exception ex)
                    {
                        Utility.writeLog(ex.ToString());
                    }

                }
            }

            return result;


        }
    }
}
