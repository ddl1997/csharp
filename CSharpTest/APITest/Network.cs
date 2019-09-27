using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace CSharpTest
{
    public class Network
    {
        #region Http

        public void Post(string Url, string jsonParas, Func<HttpWebRequest, HttpWebRequest> settings)
        {
            string strURL = Url;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            string paraUrlCoded = jsonParas;//System.Web.HttpUtility.UrlEncode(jsonParas);   

            byte[] payload;
            payload = Encoding.UTF8.GetBytes(paraUrlCoded); 
            request.ContentLength = payload.Length;

            Stream writer;
            try
            {
                writer = request.GetRequestStream();
            }
            catch (Exception)
            {
                writer = null;
                Console.Write("连接服务器失败!");
            }
            writer.Write(payload, 0, payload.Length);
            writer.Close();

            request = settings(request);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;
            }
            Stream s = response.GetResponseStream();
            StreamReader sRead = new StreamReader(s);
            string postContent = sRead.ReadToEnd();
            sRead.Close();

            sw.Stop();
            Console.WriteLine("Thread output: {0}, time cost {1}", postContent, sw.ElapsedMilliseconds);
        }

        #endregion
    }
}
