using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Car_Rental_System.Models;
namespace Car_Rental_System.Controllers
{
    public class CarController : ApiController
    {
        FinalProject_DBEntities dalobj = new FinalProject_DBEntities();
        // GET: api/Car
        public Response Get()
        {
            Response response = new Response();

            List<Car> CarList = dalobj.Cars.ToList();
            response.Data = CarList;
            response.Error = null;
            response.Status = "Success";
            return response;
        }

        // GET: api/Car/5
        public Response Get(int id)
        {
            Response response = new Response();
            Car car = dalobj.Cars.Find(id);
            response.Data = car;
            response.Error = null;
            response.Status = "Success";
            return response;
        }

        // POST: api/Car
        public Response Post([FromBody]Car car)
        {
            Response response = new Response();
            var result = dalobj.Cars.Add(car);
            dalobj.SaveChanges();
            response.Data = result;
            response.Error = null;
            response.Status = "Success";
            return response;

        }

        // PUT: api/Car/5
        public Response Put(int id, [FromBody]Car car)
        {
            Response response = new Response();
            Car car_to_be_changed = dalobj.Cars.Find(id);
            car_to_be_changed.Brand = car.Brand;
            car_to_be_changed.DailyPrice = car.DailyPrice;
            car_to_be_changed.FuelType = car.FuelType;
            car_to_be_changed.ImgSrc = car.ImgSrc;
            car_to_be_changed.Mileage = car.Mileage;
            car_to_be_changed.Model = car.Model;
            car_to_be_changed.SeatQuantity = car.SeatQuantity;
            car_to_be_changed.Stock = car.Stock;
            car_to_be_changed.Type = car.Type;
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

        // DELETE: api/Car/5
        public Response Delete(int id)
        {
            Response response = new Response();
            Car u1 = dalobj.Cars.Find(id);
            dalobj.Cars.Remove(u1);
            dalobj.SaveChanges();
            response.Data = null;
            response.Status = "Success";
            response.Error = null;
            return response;
        }
    }
}
