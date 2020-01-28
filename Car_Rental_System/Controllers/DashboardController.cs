using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Car_Rental_System.Models;
namespace Car_Rental_System.Controllers
{
    public class DashboardController : ApiController
    {
        FinalProject_DBEntities dalobj = new FinalProject_DBEntities();

        DashboardController()
        {
            dalobj.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/Dashboard
        [HttpGet]
        [Route("api/Dashboard/UserList")]
        public Response UserList()
        {
            Response response = new Response();
            List<T_Users> userList = dalobj.T_Users.ToList();
            response.Data = userList;
            response.Status = "Success";
            response.Error = null;
            return response;
        }

        

        [HttpGet]
        [Route("api/Dashboard/PasswordLog")]
        public Response PasswordLog()
        {
            Response response = new Response();
            List<T_PasswordHistoryLog> PasswordLog = dalobj.T_PasswordHistoryLog.ToList();
            response.Data = PasswordLog;
            response.Status = "Success";
            response.Error = null;
            return response;
        }

        [HttpGet]
        [Route("api/Dashboard/Feedback")]
        public Response Feedback()
        {
            Response response = new Response();
            List<T_Feedback> Feedbackl = dalobj.T_Feedback.ToList();
            response.Data = Feedbackl;
            response.Status = "Success";
            response.Error = null;
            return response;
        }

        [HttpPost]
        [Route("api/Dashboard/Feedback")]
        public Response Post([FromBody]T_Feedback fb)
        {
            Response response = new Response();
            var result = dalobj.T_Feedback.Add(fb);
            dalobj.SaveChanges();
            response.Data = result;
            response.Error = null;
            response.Status = "Success";
            return response;

        }

        #region notused
        // GET: api/Dashboard/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Dashboard
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Dashboard/5

        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Dashboard/5
        public void Delete(int id)
        {
        } 
        #endregion
    }
}
