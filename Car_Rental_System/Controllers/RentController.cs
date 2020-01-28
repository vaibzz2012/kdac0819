using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Car_Rental_System.Models;

namespace Car_Rental_System.Controllers
{
    public class RentController : ApiController
    {
        FinalProject_DBEntities dalobj = new FinalProject_DBEntities();

        public RentController()
        {
            dalobj.Configuration.ProxyCreationEnabled = false;

        }
        // GET: api/Rent
        public Response Get()
        {
            Response response = new Response();

            List<T_Rent> RentList = dalobj.T_Rent.ToList();
            response.Data = RentList;
            response.Error = null;
            response.Status = "Success";
            return response;
        }

        // GET: api/Rent/5
        public Response Get(int id)
        {
            Response response = new Response();
            T_Rent resv = dalobj.T_Rent.Find(id);
            response.Data = resv;
            response.Error = null;
            response.Status = "Success";
            return response;
        }

        public Response Post([FromBody]T_Rent t1)
        {
            Response response = new Response();
            var result = dalobj.T_Rent.Add(t1);
            dalobj.SaveChanges();
            response.Data = result;
            response.Error = null;
            response.Status = "Success";
            return response;

        }

        // PUT: api/Rent/5
        public Response Put(int id, [FromBody]T_Rent t1)
        {
            Response response = new Response();
            T_Rent res_to_be_changed = dalobj.T_Rent.Find(id);

            res_to_be_changed.TotalRentDay = t1.TotalRentDay;
            res_to_be_changed.DailyRentFee = t1.DailyRentFee;
            res_to_be_changed.Total = t1.Total;
            res_to_be_changed.Refund = t1.Refund;
            res_to_be_changed.CarId = t1.CarId;
            res_to_be_changed.UserId = t1.UserId;
            res_to_be_changed.ReservationId = t1.ReservationId;
            var res = dalobj.SaveChanges();
            if (res != 0)
            {

                response.Data = res;
                response.Status = "Success";
                response.Error = null;
                return response;
            }
            else
            {

                response.Data = null;
                response.Status = "Failed";
                response.Error = null;
                return response;
            }
        }

        // DELETE: api/Rent/5
        public void Delete(int id)
        {
        }
    }
}
