using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace WebServer
{
    class Program
    {
        static void Main(string[] args)
        {
                        
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://+:8080/");
            listener.Start();

            bool running = true;
            while (running)
            {
                Console.WriteLine("Listening...");
                
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;

                Console.WriteLine("req for " + context.Request.RawUrl);
                ThreadRequest tr = new ThreadRequest() { route = request.RawUrl, response = context.Response };
                
                ThreadPool.QueueUserWorkItem(new WaitCallback(SendResponse), tr);

                
            }
            
        }

        static void SendResponse(Object tr)
        {
            JavaScriptSerializer jser = new JavaScriptSerializer();
            ThreadRequest threadrequest = tr as ThreadRequest;

            string responseString = "";
            switch (threadrequest.route)
            {
                case "/records":
                    List<string> records = new List<string>() { "recA", "recB", "recC" };
                    responseString = jser.Serialize(records);
                    break;
                case "/sugar":
                    responseString = "ahh sugar";
                    break;
            }
            
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);

            threadrequest.response.ContentLength64 = buffer.Length;
            threadrequest.response.AddHeader("Access-Control-Allow-Origin", "*");
            System.IO.Stream output = threadrequest.response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();

        }
        
    }

    class ThreadRequest
    {
        public HttpListenerResponse response;
        public string route;
    }
}
