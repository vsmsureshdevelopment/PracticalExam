using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace EC.Models
{
    [XmlRoot("Expense")]

    public class Expense    
    {
        [XmlElement("cost_centre")]
        public string CostCentre { get; set; }

        [XmlElement("total")]

        public decimal Total { get; set; }

        [XmlElement("payment_method")]

        public string PaymentMethod { get; set; }

        public string failureMessage { get; set; }

    }
}
