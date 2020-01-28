using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Car_Rental_System.Models;
using System.Net.Mail;

namespace Car_Rental_System.Controllers
{
    public class UserController : BaseController
    {
        UserController()
        {
            dalobj.Configuration.ProxyCreationEnabled = false;
        }

        FinalProject_DBEntities dalobj = new FinalProject_DBEntities();
        // GET: api/User
        public IEnumerable<T_Users> Get()
        {
            return dalobj.T_Users.ToList();
        }

        // GET: api/User/5
        public Response Get(int id)
        {
            T_Users u1 = dalobj.T_Users.Find(id);
            Response response = new Response();
            
            response.Data = u1;
            response.Error = null;
            response.Status = "Success";
            return response;
        }

        // POST: api/User
        [HttpPost]
        [Route("api/User/UserAdd")]
        public Response UserAdd([FromBody]T_Users u)
        {
            Response response = new Response();
            u.RoleId = 2;
            var result = dalobj.T_Users.Add(u);
            dalobj.SaveChanges();
            if (result != null)
            {
                Email e = new Email();
                response.Data = result;
                response.Error = null;
                response.Status = "Success";
                e.to = result.EmailId;
                e.subject = "RIDERS RENTALS REGISTERATION";
                e.body = "Cheers Rider B) , Thank you for Registering. Have a Happy & Safe Riding";
                SendEmail(e);
                return response;
            }
            else
            {
                response.Data = null;
                response.Error = null;
                response.Status = "Failed";
                return response;
            }
        }

        // PUT: api/User/5
        [HttpPut]
        [Route("api/User/UserUpdate/{id}")]
        public Response UserUpdate(int id, [FromBody]T_Users u)
        {
            Response response = new Response();
            T_Users user_to_be_changed = dalobj.T_Users.Find(id);
            user_to_be_changed.EmailId = u.EmailId;
            user_to_be_changed.Name = u.Name;
            user_to_be_changed.MobileNo = u.MobileNo;
            //user_to_be_changed.Password = u.Password;
            var res = dalobj.SaveChanges();
            response.Data = res;
            response.Status = "Success";
            response.Error = null;
            return response;
        }

        // DELETE: api/User/5
        [HttpDelete]
        [Route("api/User/UserDelete")]
        public Response UserDelete(int id)
        {
            Response response = new Response();
            T_Users u1 = dalobj.T_Users.Find(id);
            dalobj.T_Users.Remove(u1);
            dalobj.SaveChanges();
            response.Data = null;
            response.Status = "Success";
            response.Error = null;
            return response;
        }

        [HttpPut]
        [Route("api/User/confirmation/{id}")]
        public Response confirmation(int id,[FromBody]T_Users u)
        {
            Response response = new Response();
            T_Users u1 = dalobj.T_Users.Find(id);
            Email e = new Email();
            response.Data = null;
            response.Error = null;
            response.Status = "Success";
            e.to = u1.EmailId;
            e.subject = "RIDERS RENTALS RENT CONFIRMATION";
            e.body = "Hello "+ u1.Name +", Payment successful & your car is ready for pickup";
            SendEmail(e);

            return response;

        }
        


        //------------------------------------------------------------------------------------------//

        [HttpPost]
        [Route("api/User/OTP")]
        public Response OTP([FromBody]dynamic otpDetails)
        {
            string email = otpDetails.EmailId.ToString();


            var userlist = dalobj.T_Users.ToList();
            var User = (from u in userlist
                        where u.EmailId == email
                        select u).SingleOrDefault();
            string o = otpDetails.OTP.ToString();

            var otpd = dalobj.T_OTP_Details.ToList();
            var vOTP = (from v in otpd
                        where v.UserId == User.UserId && v.OTP == o
                        select v).SingleOrDefault();

            if (vOTP != null)
            {
                Response RC = new Response();
                RC.Status = "success";
                RC.Error = null;
                RC.Data = vOTP;
                return RC;
            }
            else
            {
                Response RC = new Response();
                RC.Status = "fail";
                RC.Error = null;
                RC.Data = false;
                return RC;
            }
        }




        [HttpPost]
        [Route("api/User/IsExist")]
        public Response IsExist([FromBody]T_Users value)
        {
            Email e = new Email();
            
            var userlist = dalobj.T_Users.ToList();
            var User = (from u in userlist
                        where u.EmailId == value.EmailId
                        select u).SingleOrDefault();
            if (User != null)
            {
                Response RC = new Response();
                string mails = GetOTP();
                e.to = User.EmailId;
                e.subject = "OTP";
                e.body = mails;
                T_OTP_Details otp = new T_OTP_Details();
                otp.UserId = User.UserId;
                otp.ValidTill = DateTime.Now;
                otp.GeneratedOn = DateTime.Now;
                otp.OTP = mails;
                dalobj.T_OTP_Details.Add(otp);
                dalobj.SaveChanges();
                //SendEmail("vermavaibhav2012@gmail.com", "hello", "hello");
                SendEmail(e); //mails body me jaega

                RC.Status = "success";
                RC.Error = null;
                RC.Data = mails;
                return RC;
            }
            else
            {
                Response RC = new Response();
                RC.Status = "fail";
                RC.Error = null;
                RC.Data = false;
                return RC;
            }

        }


        [HttpPut]
        [Route("api/User/UpdatePassword")]
        public Response updatepassword([FromBody]T_Users value)
        {

            var userlist = dalobj.T_Users.ToList();
            var User = (from u in userlist
                        where u.EmailId == value.EmailId
                        select u).SingleOrDefault();

            if (User != null)
            {
                User.Password = value.Password;
                dalobj.SaveChanges();
                Response rc = new Response();
                rc.Status = "success";
                rc.Error = null;
                rc.Data = User;
                return rc;
            }
            else
            {
                Response rc = new Response();
                rc.Status = "fail";
                rc.Error = null;
                rc.Data = null;
                return rc;
            }
        }
        private string GetOTP(string otpType = "1", int len = 5)
        {
            //otptype 1 = alpha numeric
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = numbers;
            if (otpType == "1")
            {
                characters += alphabets + small_alphabets + numbers;
            }
            int length = 5;
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            return otp;
        }

        [HttpPost]
        [Route("api/User/SendMail")]
        public Response SendEmail(Email e)
        {
            Response response = new Response();
            string to = e.to;
            string subject = e.subject;
            string body = e.body;

            string result = "Message Sent Successfully..!!";
            string senderID = "riderrentals555@gmail.com";// use sender’s email id here..
            const string senderPassword = "9425520046"; // sender password here…
            try
            {
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com", // smtp server address here…
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential(senderID, senderPassword),
                    Timeout = 30000,
                };
                MailMessage message = new MailMessage(senderID, to, subject, body);
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                result = "Error sending email.!!!";
                response.Error = ex;
            }
            return response;

        }

        //[HttpPost]
        //[Route("api/User/SendMail")]
        //public string SendEmail(string toAddress,string subject,string body)
        //{
        //    Response response = new Response();


        //    string result = "Message Sent Successfully..!!";
        //    string senderID = "romanreignspunch@gmail.com";// use sender’s email id here..
        //    const string senderPassword = "9425520046"; // sender password here…
        //    try
        //    {
        //        SmtpClient smtp = new SmtpClient
        //        {
        //            Host = "smtp.gmail.com", // smtp server address here…
        //            Port = 587,
        //            EnableSsl = true,
        //            DeliveryMethod = SmtpDeliveryMethod.Network,
        //            Credentials = new System.Net.NetworkCredential(senderID, senderPassword),
        //            Timeout = 30000,
        //        };
        //        MailMessage message = new MailMessage(senderID, toAddress, subject, body);
        //        smtp.Send(message);
        //    }
        //    catch (Exception ex)
        //    {
        //        result = "Error sending email.!!!";
        //        response.Error = ex;
        //    }
        //    return result;

        //}

    }

}

