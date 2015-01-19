using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using TicketRobot.Core;
using TicketRobot.Service;
using TicketRobot.ViewModel;
namespace TicketRobot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //轉換網址

            AEFlightService aeService = new AEFlightService();

            //從今天開始 連續執行三個月
            DateTime srtDate = DateTime.Now;
            DateTime endDate = srtDate.AddMonths(3);

            string[] cities = { "TSA", "RMQ", "KHH", "HUN", "TTT", "KNH", "MZG" };
            foreach (string depCity in cities)
            {
                foreach (string arrCity in cities)
                {
                    if (depCity == arrCity) continue;

                    for (int i = 0; i < (endDate - srtDate).Days + 1; i++)
                    {
                        var sDate = srtDate.AddDays(i);
                        AERequestViewModel req = new AERequestViewModel();
                        req.region = "國內";
                        req.in_d_city = depCity;
                        req.in_a_city = arrCity;
                        req.departureMonth = sDate.ToString("yyyy-MM");
                        req.departureDay = sDate.ToString("dd");
                        req.returnMonth = sDate.ToString("yyyy-MM");
                        req.returnDay = sDate.ToString("dd");
                        req.in_fit_o = "o";
                        req.in_stops = "A";
                        req.in_dptarv = "D";
                        req.currentCalForm = "dep";
                        req.currentdate = "115-1-17";
                        HtmlAgilityPack.HtmlDocument doc = Utility.downLoadHtmlDoc(AppSettings.AEUrl, aeService.setPostBody(req), Encoding.GetEncoding("Big5"));
                        req.currentdate = doc.DocumentNode.OuterHtml;
                        var result = aeService.parse(doc);
                        if (result.Count > 0)
                        {
                            Utility.writeFile(sDate.ToString("yyyyMMdd") + depCity + arrCity, JsonConvert.SerializeObject(result));
                        }
                    }
                }
            }
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            
        }
    }
}
