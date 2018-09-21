using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Northwind.Entities.Models;
using System.Web.Http;
using System.Web.Mvc;
using Northwind.Service;
using Repository.Pattern.UnitOfWork;

namespace NorthWind.Api.CustomFilter
{
    public class CustomExceptionHandlerAttribute:FilterAttribute, IExceptionFilter
    {

        //private ExceptionLoggerService _iExceptionLoggerService;
        //private IUnitOfWork _unitOfWork;
        //private IUnitOfWorkAsync _unitOfWorkAsync;

        //public ExceptionHandlerAttribute(IExceptionLoggerService iExceptionLoggerService, IUnitOfWork unitOfWork, IUnitOfWorkAsync unitOfWorkAsync)
        //{
        //    this._iExceptionLoggerService = iExceptionLoggerService;
        //    this._unitOfWork = unitOfWork;
        //    this._unitOfWorkAsync = unitOfWorkAsync;
        //}

        public  void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                ExceptionLogger logger = new ExceptionLogger()
                {
                    ExceptionMessage = filterContext.Exception.Message,
                    ExceptionStackTrace = filterContext.Exception.StackTrace,
                   // ControllerName = filterContext.RouteData.Values["Controller"].ToString(),
                  //  LogTime = DateTime.Now.ToString(),
                   // RequestMethod = filterContext.RequestContext.HttpContext.ToString()

                };
                //_iExceptionLoggerService.Insert(logger);
                //_unitOfWorkAsync.SaveChangesAsync();
                //filterContext.ExceptionHandled = true;
                NorthwindContext nd = new NorthwindContext();
                nd.ExceptionLogger.Add(logger);
                nd.SaveChanges();
                filterContext.ExceptionHandled = true;
            }
        }

    }
}
