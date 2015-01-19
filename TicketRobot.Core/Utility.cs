using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO; 
using HtmlAgilityPack;
using System.Threading;

namespace TicketRobot.Core
{
    public class Utility
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>載入提供的網址的HTML並轉換成HTML Document-Get</summary>
        /// <param name="url">網址</param>
        /// <param name="encoding">編碼類型</param>
        public static HtmlDocument downLoadHtmlDoc(string url, Encoding encoding)
        {
            WebClient client = new WebClient();
            HtmlDocument doc = new HtmlDocument();
            try
            {
                using (MemoryStream ms = new MemoryStream(client.DownloadData(url)))
                {
                    Thread.Sleep(100);
                    //使用UTF8反而會變亂碼，要用Default
                    doc.Load(ms, encoding);
                }
            }
            catch (Exception ex)
            {
                logger.Trace(string.Format("Download url 失敗 : {0}", url));
            }
            return doc;
        }

        /// <summary>載入提供的網址的HTML並轉換成HTML Document-Post</summary>
        /// <param name="url">網址</param>
        /// <param name="encoding">編碼類型</param>
        public static HtmlDocument downLoadHtmlDoc(string url, string postBody, Encoding encoding)
        {
            WebClient client = new WebClient();
            HtmlDocument doc = new HtmlDocument();
            try
            {
                client.Encoding = encoding;
                client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
                byte[] sendData = encoding.GetBytes(postBody);
                using (MemoryStream ms = new MemoryStream(client.UploadData(url, "POST", sendData)))
                {
                    Thread.Sleep(100);
                    //使用UTF8反而會變亂碼，要用Default
                    doc.Load(ms, encoding);
                }
            }
            catch (Exception ex)
            {
                logger.Trace(string.Format("Download url 失敗 : {0}", url));
            }
            return doc;
        }

        public static void writeErrorLog(string content)
        {
            logger.Error(content);
        }

        public static void writeLog(string content)
        {
            logger.Info(content);
        }
        public static void writeFile(string fileName, string content)
        {
            string filePath = "E:/Logs/"+DateTime.Now.ToString("yyyyMMdd")+"/";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            filePath =  filePath+fileName+ ".json";
           
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {

                    sw.Write(content);
                }
            }
        }
    }
}
