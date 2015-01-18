using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            AERequestViewModel req = new AERequestViewModel();
            req.region = "國內";
            req.in_d_city = "TSA";
            req.in_a_city = "KNH";
            req.departureMonth = "2015-01";
            req.departureDay = "17";
            req.returnMonth = "2015-01";
            req.returnDay = "27";
            req.in_fit_o = "o";
            req.in_stops = "A";
            req.in_dptarv = "D";
            req.currentCalForm = "dep";
            req.currentdate = "115-1-17";
            HtmlAgilityPack.HtmlDocument doc = Utility.downLoadHtmlDoc(AppSettings.AEUrl,  aeService.setPostBody(req), Encoding.GetEncoding("Big5"));
            req.currentdate = doc.DocumentNode.OuterHtml;
            aeService.parse(doc);

        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            
        }
    }
}
