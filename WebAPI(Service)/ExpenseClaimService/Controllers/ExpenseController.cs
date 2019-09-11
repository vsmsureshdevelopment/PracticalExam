using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EC.Models;
using EC.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseClaimService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : Controller
    {
        

        [HttpPost("api/expense/CalculateExpenseDetails")]
        public ActionResult<Expense> CalculateExpenseDetails(EmailContent emailContent)
        {

            ClaimService service = new ClaimService();
            return service.calculateExpenses(emailContent);
        }

        [HttpGet("{emailBody}")]
        public ActionResult<Expense> Get(string emailBody)
     { 
            EmailContent emailContent = new EmailContent();
            emailContent.EmailBody=emailBody;
            ClaimService service = new ClaimService();
            return service.calculateExpenses(emailContent);
        }
    }
}