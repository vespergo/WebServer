using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleHTTPServer server = new SimpleHTTPServer(@".", 8081);
            while (Console.ReadLine() != "stop")
            {
                
            }

            server.Stop();
        }
    }
}
