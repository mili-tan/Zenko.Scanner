using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Zenko
{
    class SubdomainScanner
    {
        public static Dictionary<string,IPAddress[]> Scan(string domain,IEnumerable<string> dict)
        {
            Dictionary<string, IPAddress[]> result = new Dictionary<string, IPAddress[]>();

            foreach (var item in dict)
            {

                string subName = $"{item}.{domain}";
                try
                {
                    var dnsIPs = Dns.GetHostAddresses(subName);
                    result.Add(item,dnsIPs);
                    Console.WriteLine($"{subName} : {string.Join(", ", Array.ConvertAll(dnsIPs, s => s.ToString()))}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{subName} : {e.Message}");
                }
            }

            return result;
        }
    }
}
