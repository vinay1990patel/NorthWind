using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Northwind.Service;
using Northwind.Entities.Models;
using Repository.Pattern.Infrastructure;
using System.Data.Entity.Infrastructure;
using Northwind.Repository.Models;

using System.Threading.Tasks;
using Repository.Pattern.UnitOfWork;
using NorthWind.Api.CustomFilter;

namespace NorthWind.Api.Controllers
{
    public class EmployeeController : ApiController
    {
    }
}
