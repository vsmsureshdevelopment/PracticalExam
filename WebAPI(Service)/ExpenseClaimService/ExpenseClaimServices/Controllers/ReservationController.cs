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
    public class ReservationController : ApiController
    {

        public HttpResponseMessage GetReservationDetails(string emailBody)
        {
            Reservation reservation = new Reservation();
            EmailContent emailContent = new EmailContent();
            emailContent.EmailBody = emailBody;
            ClaimService service = new ClaimService();
            try
            {
                reservation = service.GetReservationDetails(emailContent);
                if (reservation != null)
                {
                    return Request.CreateResponse<Reservation>(HttpStatusCode.OK, reservation);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }



    }
}
