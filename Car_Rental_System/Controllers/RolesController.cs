using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Car_Rental_System.Models;
namespace Car_Rental_System.Controllers
{
    public class RolesController : ApiController
    {
        FinalProject_DBEntities dalobj = new FinalProject_DBEntities();
           
        // GET: api/Roles
        public IEnumerable<T_Roles> Get()
        {
           
            return dalobj.T_Roles.ToList();
           
        }

        // GET: api/Roles/5
        public T_Roles Get(int id)
        {
            T_Roles Role = dalobj.T_Roles.Find(id);
            return Role;
        }

        // POST: api/Roles
        public void Post([FromBody]T_Roles r)
        {
            dalobj.T_Roles.Add(r);
            dalobj.SaveChanges();
        }

        // PUT: api/Roles/5
        public void Put(int id, [FromBody]T_Roles r)
        {
            T_Roles Role_to_be_changed = dalobj.T_Roles.Find(id);
            Role_to_be_changed.RoleName = r.RoleName;
            dalobj.SaveChanges();
        }

        // DELETE: api/Roles/5
        public void Delete(int id)
        {
            T_Roles r1 = dalobj.T_Roles.Find(id);
            dalobj.T_Roles.Remove(r1);
            dalobj.SaveChanges();
        }
    }
}
