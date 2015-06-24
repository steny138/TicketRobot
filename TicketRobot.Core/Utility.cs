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
        /// <summary>
        /// 載入提供的網址的HTML並轉換成HTML Document-Get
        /// </summary>
        /// <param name="url">網址</param>
        /// <param name="encoding">編碼類型</param>
        /// <returns>HTML Document </returns>
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

        /// <summary>
        /// 載入提供的網址的HTML並轉換成HTML Document-Post
        /// </summary>
        /// <param name="url">網址</param>
        /// <param name="postBody">The post body.</param>
        /// <param name="encoding">編碼類型</param>
        /// <returns>HTML Document</returns>
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

        /// <summary>
        /// 紀錄錯誤LOG
        /// </summary>
        /// <param name="content">錯誤訊息</param>
        public static void writeErrorLog(string content)
        {
            logger.Error(content);
        }

        /// <summary>
        /// 紀錄LOG
        /// </summary>
        /// <param name="content">LOG內容</param>
        public static void writeLog(string content)
        {
            logger.Info(content);
        }
        /// <summary>
        /// 寫入LOG檔案
        /// </summary>
        /// <param name="fileName">LOG檔案名稱</param>
        /// <param name="content">LOG內容</param>
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
