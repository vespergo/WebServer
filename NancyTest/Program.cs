using Nancy;
using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NancyTest
{
    // a simple module to be hosted in the console app
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get["/"] = x => { return "Hello World"; };
            Get["/{category}"] = parameters => "My category is " + parameters.category;
            Get[@"/products/(?<id>[\d]{1,7})"] = parameters =>
            {
                return "Return id: " + parameters.id;
            };
            Post["data"] = _ =>
            {
                
                return "";
            };
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //(from admin cmd) http add urlacl url=http://+:8080/ user=Everyone
            var host = new NancyHost(new Uri("http://localhost:8080"));
            host.Start(); // start hosting

            Console.WriteLine("Listening...");

            Console.ReadKey();
            host.Stop();  // stop hosting
        }
    }
}
