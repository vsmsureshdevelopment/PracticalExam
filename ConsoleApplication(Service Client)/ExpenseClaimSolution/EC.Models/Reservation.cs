using System;
using System.Collections.Generic;
using System.Text;

namespace EC.Models
{
   public class Reservation
    {
        public string Vendor { get; set; }

        /// <summary>
        /// Purpose of the reservation
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Reservation Date
        /// </summary>
        public string Date { get; set; }

        public string failureMessage { get; set; }
    }
}
