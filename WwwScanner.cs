using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace wwwscan
{
    class WwwScanner
    {
        public static List<string> Scan(string domain, IEnumerable<string> dict)
        {
            List<string> result = new List<string>();

            foreach (var item in dict)
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest) WebRequest.Create(domain + item);
                    request.AllowAutoRedirect = false;
                    HttpWebResponse response = (HttpWebResponse) request.GetResponse();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        result.Add(domain + item);
                    }
                    else
                        Console.ForegroundColor = ConsoleColor.Gray;

                    Console.WriteLine(
                        $"{Convert.ToInt32(response.StatusCode)} {response.StatusCode} : {domain + item}");
                    response.Close();
                }
                catch (WebException e)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    HttpWebResponse response = (HttpWebResponse) e.Response;
                    try
                    {
                        Console.WriteLine(
                            $"{Convert.ToInt32(response.StatusCode)} {response.StatusCode} : {domain + item}");
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine($"{exception.Message} : {domain + item}");
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"{exception.Message} : {domain + item}");
                }
            }

            return result;
        }
    }
}
