using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
            
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser browser = (WebBrowser)sender;
            string url = browser.Url.LocalPath;
            switch(url)
            {
                case "/uniairec/b2c/cfresav01.aspx":
                    //首頁
                    insertText("UniCalendar1_txtYear", "2015"); //去程日期 - 年
                    insertText("UniCalendar1_txtMonth", "01"); //去程日期 - 月
                    insertText("UniCalendar1_txtDay", "30"); //去程日期 - 日
                    insertText("UniCalendar2_txtYear", "2015"); //回程日期 - 年
                    insertText("UniCalendar2_txtMonth", "01"); //回程日期 - 月
                    insertText("UniCalendar2_txtDay", "30"); //回程日期 - 日
                    insertText("Radio4", "1"); //回程日期 - 日
                    
                    setSelectOption("item1", "TSA"); //出發地
                    
                    setSelectOption("item2", "TTT"); //目的地
                    setSelectOption("ddm_adultNum", "1"); //成人
                    setSelectOption("ddm_childNum", "0"); //兒童
                    setSelectOption("ddm_oldNum", "0"); //敬老
                    setSelectOption("ddm_offIndNum", "0"); //軍人
                    setSelectOption("ddm_SBNum", "0"); //愛心
                    setSelectOption("ddm_offIndNum", "0"); //居民
                    setSelectOption("ddm_IENum", "0"); //愛心陪同
                    buttonClick("Button1");
                    break;
                case "/uniairec/b2c/cfresav02.aspx":
                    //選擇航班
                    buttonClick("Button1");
                    break;
                case "/uniairec/b2c/cfresav03a.aspx":
                    //閱讀注意事項
                    checkboxClick("checkbox");
                    buttonClick("Button1");
                    break;
                case "/uniairec/b2c/cfresav03.aspx":
                    //旅客資料
                    insertText("txt_famName_1", "劉"); //姓
                    insertText("txt_gvnName_1", "宥辰"); //名
                    insertText("ddm_year_1", "1989"); //生日 - 年
                    insertText("ddm_month_1", "05"); //生日 - 月
                    insertText("ddm_day_1", "04"); //生日 - 日
                    insertText("txt_idNum_1", "N124885927"); //身分證
                    insertText("txt_pptNum_1", ""); //外籍旅客護照號碼
                    setSelectOption("ddm_title_1", "01");

                    //---------聯絡人資料
                    setSelectOption("samewith", "1"); //同第幾位旅客資料
                    insertText("areacode", "02"); //區碼
                    insertText("phonenum", "87939000"); //市內電話
                    insertText("ext", "8338"); //分機
                    insertText("cellnum", "0911757685"); //手機號碼
                    setSelectOption("cellnum_na", "886/"); //國碼
                    insertText("email", "steny138@gmail.com"); //電子郵件
                    

                    //buttonClick("Button1");
                    break;

            }
            url = "te";
        }

        #region 輸入欄位
        public void insertText(string element, string value)
        {
            try
            {
                webBrowser.Document.Body.All[element].InnerText = value;
                //輸入至欄位
            }
            catch
            {
                //status(result);
                //發生錯誤，輸出至狀態
            }
        }

        public void setSelectOption(string element, string value)
        {
            try
            {
                var dropdown = webBrowser.Document.GetElementById(element);
                if (dropdown == null)
                {
                    dropdown = webBrowser.Document.GetElementsByTagName("select")[element];
                }

                if (dropdown != null)
                {
                    var dropdownItems = dropdown.GetElementsByTagName("option");

                    foreach (HtmlElement option in dropdownItems)
                    {
                        var originValue = option.GetAttribute("value").ToString();
                        if (originValue.Equals(value))
                        {
                            option.SetAttribute("selected", "selected");
                            dropdown.RaiseEvent("onChange");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                
            }
        }

        public void buttonClick(string id)
        {
            webBrowser.Document.GetElementById(id).InvokeMember("click");
        }

        public void checkboxClick(string id)
        {
            var checkbox = webBrowser.Document.GetElementsByTagName("input").GetElementsByName(id);
            if (checkbox != null && checkbox.Count > 0)
            {
                checkbox[0].InvokeMember("click");
            }
        }
        #endregion

        #region 按鈕事件
       
        private void SpiderButton_Click(object sender, EventArgs e)
        {
            SpiderButton.Text = "Executing...";
            OrderButton.Enabled = false;
            SpiderButton.Enabled = false;
            ThreadPool.QueueUserWorkItem(new WaitCallback(startJob), "0");
        }
        private void OrderButton_Click(object sender, EventArgs e)
        {
            OrderButton.Text = "Executing...";
            OrderButton.Enabled = false;
            SpiderButton.Enabled = false;
            webBrowser.Visible = true;
            //自動操作
            webBrowser.Navigate("https://www.uniair.com.tw/uniairec/b2c/cfresav01.aspx");
            //ThreadPool.QueueUserWorkItem(new WaitCallback(startOrder), "0");
        }
        private void startOrder(object state)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Utility.writeErrorLog(string.Format("程式錯誤: {0}", ex.ToString()));
            }
            finally
            {
                this.Invoke(
                 new UpdateLableHandler(finishExecuteOrder),
                 "1");
            }
        }

        private void startJob(object state)
        {
            try
            {
                //轉換網址
                AEFlightService aeService = new AEFlightService();

                //從今天開始 連續執行三個月
                DateTime srtDate = DateTime.Now;
                DateTime endDate = srtDate.AddMonths(3);

                //string[] cities = { "TSA", "RMQ", "KHH", "HUN", "TTT", "KNH", "MZG" };
                string[] cities = { "TSA", "KNH", "MZG" };
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
            catch (Exception ex)
            {
                Utility.writeErrorLog(string.Format("程式錯誤: {0}", ex.ToString()));
            }
            finally
            {
                this.Invoke(
                 new UpdateLableHandler(finishExecuteSpider),
                 "1");
            }
        }
        delegate void UpdateLableHandler(string text);
        private void finishExecuteSpider(string text)
        {
            OrderButton.Enabled = true;
            SpiderButton.Enabled = true;
            webBrowser.Visible = false;
            SpiderButton.Text = "Execute - FlightTable";
        }
        private void finishExecuteOrder(string text)
        {
            OrderButton.Enabled = true;
            SpiderButton.Enabled = true;
            webBrowser.Visible = false;
            SpiderButton.Text = "Execute - Order";
        }
        #endregion
    }
}
