using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EC.Models;
using EC.Services;

namespace ExpenseClaimServices.Controllers
{
    public class ExpenseController : ApiController
    {
        public ExpenseController()
        {
        }
        public HttpResponseMessage GetExpenseDetails(string emailBody)
        {
            Expense expense = new Expense();
            EmailContent emailContent = new EmailContent();
            emailContent.EmailBody = emailBody;
            ClaimService service = new ClaimService();
            try
            {
                expense = service.GetExpenseDetails(emailContent);
                if (expense != null)
                {
                    return Request.CreateResponse<Expense>(HttpStatusCode.OK, expense);
                }
                else
                {
                   return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
               
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
           
        }
    }
}
