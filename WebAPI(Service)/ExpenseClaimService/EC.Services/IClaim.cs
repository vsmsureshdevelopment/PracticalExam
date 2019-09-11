using EC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EC.Services
{
   public interface IClaim
    {
        Expense GetExpenseDetails(EmailContent emailText);
        Reservation GetReservationDetails(EmailContent emailText);


        
    }
}
