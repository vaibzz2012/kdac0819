using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using MyLoggerLib;
using System.Diagnostics;

namespace Car_Rental_System.Filter
{
    public class LogFilter:ActionFilterAttribute
    {
        ILogger logger = LoggerFactory.GetLogger(1);

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string msgFormat = "/{0}/{1} is calling";
            string controller = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string method = actionContext.ActionDescriptor.ActionName;
            string msg = string.Format(msgFormat, controller, method);
            logger.Log(msg);
            Debug.WriteLine(msg);
        }



        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
           
            string msgFormat = "/{0}/{1} is called";
            string controller = actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string method = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
            string msg = string.Format(msgFormat, controller, method);
            logger.Log(msg);
            Debug.WriteLine(msg);
        }

        

        
    }
}