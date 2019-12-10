using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace WXBookStoreService
{
    public class HttpHelper
    {
        public static string AppId = "wx8cced54a8b562122";
        public static string AppSecret = "f6ef8d29d215a725b4b54eddb701cd16";

        public static string HttpGet(string url, Encoding encoding = null)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            WebClient wc = new WebClient();
            wc.Encoding = encoding ?? Encoding.UTF8;
            return wc.DownloadString(url);
        }

        public static string HttpPost(string url, string parameters, Encoding encoding = null)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            WebClient wc = new WebClient();
            wc.Headers.Add("Content-Type", "application/Json");
            wc.Encoding = encoding ?? Encoding.UTF8;
            return wc.UploadString(url, parameters);
        }
    }
}