using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Car_Rental_System.Filter;
namespace Car_Rental_System.Controllers
{
    [LogFilter]
    public class BaseController : ApiController
    {

    }
}
