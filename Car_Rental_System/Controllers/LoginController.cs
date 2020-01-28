using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Car_Rental_System.Models;

namespace Car_Rental_System.Controllers
{
    public class LoginController : BaseController
    {
        LoginController()
        {
            dalobj.Configuration.ProxyCreationEnabled = false;
        }
        FinalProject_DBEntities dalobj = new FinalProject_DBEntities();
        // GET: api/Login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Login/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Login
        public Response Post([FromBody]T_Users u1)
        {
            Response res = new Response();
            if (u1.EmailId != null && u1.Password != null)
            {
                var userList = dalobj.T_Users.ToList();
                T_Users validUser = (from user in userList
                                     where user.EmailId == u1.EmailId && user.Password == u1.Password
                                     select user).SingleOrDefault();

                if (validUser != null)
                {
                    if (validUser.RoleId == 1)
                    {
                        res.Data = validUser;
                        res.Status = "SuccessAdmin";
                        res.Error = null;
                        return res;
                    }
                    else
                    {

                        res.Data = validUser;
                        res.Status = "Success";
                        res.Error = null;
                        return res;
                    }
                }
                else
                {
                    res.Error = null;
                    res.Status = "Incorrect Credentials";
                    return res;
                }

            }
            else
            {
                res.Error = null;
                res.Status = "Fields are Empty";
                return res;
            }
        }
        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
}
