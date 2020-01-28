using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Car_Rental_System.Models;

namespace Car_Rental_System.Controllers
{
    public class ReservationController : ApiController
    {
        FinalProject_DBEntities dalobj = new FinalProject_DBEntities();

        ReservationController()
        {
                    dalobj.Configuration.ProxyCreationEnabled = false;

        }

    // GET: api/Reservation
    public Response Get()
        {
            Response response = new Response();

            List<T_Reservation> ResList = dalobj.T_Reservation.ToList();
            response.Data = ResList;
            response.Error = null;
            response.Status = "Success";
            return response;
        }

        // GET: api/Reservation/5
        public Response Get(int id)
        {
            Response response = new Response();
            T_Reservation resv = dalobj.T_Reservation.Find(id);
            response.Data = resv;
            response.Error = null;
            response.Status = "Success";
            return response;
        }

        // POST: api/Reservation
        public Response Post([FromBody]T_Reservation t1)
        {
            Response response = new Response();
            var result = dalobj.T_Reservation.Add(t1);
            dalobj.SaveChanges();
            response.Data = result;
            response.Error = null;
            response.Status = "Success";
            return response;

        }
        [HttpGet]
        [Route("api/Reservation/GetSpec/{id}")]
        public Response GetSpec(int id)
        {
            Response response = new Response();
            List<T_Users> userlist = dalobj.T_Users.ToList();
            List<T_Reservation> reslist = dalobj.T_Reservation.ToList();
            var query = from user in userlist
                        join res in reslist on user.UserId equals res.UserId
                        
                        where res.UserId == id
                        select new
                        {
                            res.ReservationId,
                            res.PickupDate,
                            res.ReservationDate,
                            res.ReturnDate,
                            res.CarId
                        };
            response.Data = query;
            response.Error = null;
            response.Status = "Success";
            return response;

        }


        // PUT: api/Reservation/5
        public Response Put(int id, [FromBody]T_Reservation t1)
        {
            Response response = new Response();
            T_Reservation res_to_be_changed = dalobj.T_Reservation.Find(id);
            
            res_to_be_changed.EndTime = t1.EndTime;
            res_to_be_changed.PickupDate = t1.PickupDate;
            res_to_be_changed.ReservationDate = t1.ReservationDate;
            res_to_be_changed.ReturnDate = t1.ReturnDate;
            res_to_be_changed.StartTime = t1.StartTime;
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

        // DELETE: api/Reservation/5
        public Response Delete(int id)
        {
            Response response = new Response();
            T_Reservation u1 = dalobj.T_Reservation.Find(id);
            dalobj.T_Reservation.Remove(u1);
            dalobj.SaveChanges();
            response.Data = null;
            response.Status = "Success";
            response.Error = null;
            return response;
        }

        [HttpGet]
        [Route("api/Reservation/GetAll/{id}")]
        public Response GetAll(int id)
        {
            Response response = new Response();
              //= dalobj.T_Users.Find(id);
            List<T_Users> userlist = dalobj.T_Users.ToList();
            List<T_Reservation> reslist = dalobj.T_Reservation.ToList();
            List<Car> carlist = dalobj.Cars.ToList();
            var query = from user in userlist
                        join res in reslist on user.UserId equals res.UserId
                        join car in carlist on res.CarId equals car.CarId
                        where res.UserId == id
                        select new { user.Name, user.MobileNo, res.PickupDate, res.ReservationDate, res.ReturnDate, car.DailyPrice, car.ImgSrc, car.Model, car.Brand, car.Mileage };


            response.Data = query;
            response.Status = "Success";
            response.Error = null;
            return response;
        }
    }
}
