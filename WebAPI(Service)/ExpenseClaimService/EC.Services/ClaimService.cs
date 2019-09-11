using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using EC.Models;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace EC.Services
{
    public class ClaimService : IClaim
    {
        public Expense GetExpenseDetails(EmailContent emailText)
        {
            Expense expense = new Expense();
            expense.failureMessage = string.Empty;//this property is used to bind all custom and other exceptions
            //Use regular expression pattern to extract the xml tags
            string pattern = @"<(\w+)>.*?</\1>";
            MatchCollection matches = Regex.Matches(emailText.EmailBody, pattern);
            if (matches.Count > 0)
            {
                string xmlstring = matches[0].ToString();
                XmlDocument xml = new XmlDocument();

                try
                {
                   
                    xml.LoadXml(xmlstring);
                }
                catch (XmlException ex)
                {
                    expense.failureMessage = ex.Message;
                }
                if (expense.failureMessage == string.Empty)
                {
                    xml.LoadXml(xmlstring);
                    XmlNodeList xnList = xml.SelectNodes("/expense");
                    if (xnList.Count >0 )
                    {
                        foreach (XmlNode xn in xnList)
                        {
                            if (xn["total"] != null)
                            {
                                string costCentre = xn["cost_centre"] != null ? xn["cost_centre"].InnerText : "UNKNOWN";
                                string total = xn["total"].InnerText;
                                string paymentMethod = xn["payment_method"] != null ? xn["payment_method"].InnerText : "";
                                expense.CostCentre = costCentre;
                                expense.PaymentMethod = paymentMethod;
                                expense.Total = Convert.ToDecimal(total);
                            }
                            else
                            {
                                expense.failureMessage = "total tag is missing";
                            }
                            
                        }
                    }
                    else
                    {
                        expense.failureMessage = "expenses closing tag is missing";
                    }

                }
                

            }
            else
            {
                expense.failureMessage = "Invalid Email Content.";
            }

            return expense;
        }

        public Reservation GetReservationDetails(EmailContent emailText)
        {

            Reservation reservation = new Reservation();
            reservation.failureMessage = string.Empty;
            string pattern = @"<(\w+)>.*?</\1>";
            MatchCollection matches = Regex.Matches(emailText.EmailBody, pattern);
            string xmlString = string.Empty;
            if (matches.Count == 4)//Expense, Vendor ,Description and Date 
            {
                xmlString = matches[1].ToString();
                XmlDocument xml = new XmlDocument();

                xml.LoadXml(xmlString);
                XmlNodeList xnList = xml.SelectNodes("/vendor");
                foreach (XmlNode xn in xnList)
                {
                    reservation.Vendor = xn.InnerText != null ? xn.InnerText : "";
                }
                xmlString = matches[2].ToString();
                xml.LoadXml(xmlString);
                xnList = xml.SelectNodes("/description");
                foreach (XmlNode xn in xnList)
                {
                    reservation.Description = xn.InnerText != null ? xn.InnerText : "";
                }
                xmlString = matches[3].ToString();
                xml.LoadXml(xmlString);
                xnList = xml.SelectNodes("/date");
                foreach (XmlNode xn in xnList)
                {
                    reservation.Date =( xn.InnerText != null ? xn.InnerText : "1900-01-01");
                }

            }
            else
            {
                reservation.failureMessage = "Invalid Email Content.";
            }

            return reservation;
        }
    }
}
