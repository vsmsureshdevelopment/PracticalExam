using EC.Models;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExpenseClaimSolution
{
    class Program
    {
       
        static async Task Main(string[] args)
        {

            string path = getEmailContentPath();
            string expenseServiceURL= getExpenseServiceURL();
            string reservationServiceURL = getReservationServiceURL();
            Console.WriteLine("Please enter 'C' to view the claim details");
            Console.WriteLine("Please enter 'R' to view the reservation details");
            Console.WriteLine("Please enter 'E' to Exit the application");
            char serviceOption = Convert.ToChar(Console.ReadLine());
            while (true)
            {

                switch (serviceOption)
                {
                    case 'C':
                        GetExpenseDetails(path, expenseServiceURL);
                        break;
                    case 'R':
                        GetReservationDetails(path, reservationServiceURL);
                        break;
                    case 'E':
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Please enter valid option");
                        break;
                }
                Console.WriteLine("Please enter valid option");

                serviceOption = Convert.ToChar(Console.ReadLine());


            }

            Console.ReadLine();
        }

        private static void GetExpenseDetails(string path,string serviceURL)
        {
            string emailBody = string.Empty;
            EmailContent emailContent = new EmailContent();
            emailContent.EmailBody = File.ReadAllText(path);
            if (File.Exists(path))
            {
                using (var client = new WebClient())
                {

                    emailContent.EmailBody = File.ReadAllText(path);
                    client.Headers.Add("Content-Type:application/json");
                    client.Headers.Add("Accept:application/json");
                    emailBody = emailContent.EmailBody.Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace(" ", "");
                    var results = client.DownloadString(serviceURL + emailBody);
                    var data = JsonConvert.DeserializeObject<Expense>(results);

                    if (data != null)
                    {
                        if (data.failureMessage != "")
                        {
                            Console.WriteLine(data.failureMessage);
                        }
                        else
                        {
                            Console.WriteLine("Cost Centre: "+data.CostCentre);
                            Console.WriteLine("Payment method: " + data.PaymentMethod);
                            Console.WriteLine("Total: " + data.Total);
                        }
                        
                    }
                }
            }
        }

        private static void GetReservationDetails(string path, string serviceURL)
        {
            string emailBody = string.Empty;
            EmailContent emailContent = new EmailContent();
            emailContent.EmailBody = File.ReadAllText(path);
            if (File.Exists(path))
            {
                using (var client = new WebClient())
                {

                    emailContent.EmailBody = File.ReadAllText(path);
                    client.Headers.Add("Content-Type:application/json");
                    client.Headers.Add("Accept:application/json");
                    emailBody = emailContent.EmailBody.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
                    var results = client.DownloadString(serviceURL + emailBody);
                    var data = JsonConvert.DeserializeObject<Reservation>(results);

                    if (data != null)
                    {
                        if (data.failureMessage != "")
                        {
                            Console.WriteLine(data.failureMessage);
                        }
                        else
                        {
                            Console.WriteLine("Vendor: " + data.Vendor);
                            Console.WriteLine("Description: " + data.Description);
                            Console.WriteLine("Date: " + data.Date);
                        }

                    }
                }
            }
        }
        private static string getEmailContentPath()
        {
            return ConfigurationManager.AppSettings["emailContentPath"].ToString();
        }
        private static string getExpenseServiceURL()
        {
            return ConfigurationManager.AppSettings["ExpenseServiceURL"].ToString();
        }

        private static string getReservationServiceURL()
        {
            return ConfigurationManager.AppSettings["ReservationServiceURL"].ToString();
        }
    }
}
