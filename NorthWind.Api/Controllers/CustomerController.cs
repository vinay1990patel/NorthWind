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
     [Authorize]
    public class CustomerController : ApiController
    {
        private  ICustomerService _customerService;
        private  IUnitOfWork _unitOfWork;
        private IUnitOfWorkAsync _unitOfWorkAsync;

        public CustomerController(ICustomerService customerService, IUnitOfWork unitOfWork, IUnitOfWorkAsync unitOfWorkAsync)
        {
            this._customerService = customerService;
            this._unitOfWork =unitOfWork ;
            this._unitOfWorkAsync = unitOfWorkAsync;
        }


        // GET: api/Customer
        public IEnumerable<Customer> Get()
        {
            if (!ModelState.IsValid)
            {
                Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "500");

            }

            return _customerService.GetAllCustomer();
        }
        //// GET: api/Customer/companyName
        //public IEnumerable<Customer> GetcompanyName(string companyName)
        //{
        //    return _customerService.GetCustomerOrder(companyName);
        //}

        //// GET: api/Customer/Country
        //public IEnumerable<CustomerOrder> GetAllOrderByCountry(string countryName)
        //{
        //    if(!ModelState.IsValid)
        //    {
        //        Request.CreateErrorResponse(HttpStatusCode.NotFound, "404");

        //    }
        //    return _customerService.GetCustomerOrders(countryName);

        //}


        // POST: api/Customer
        [System.Web.Http.Description.ResponseType(typeof(Customer))]
        public  async Task<IHttpActionResult> PostCustomer([FromBody]Customer customer)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            customer.ObjectState = ObjectState.Added;
           _customerService.Insert(customer);
            try
            { 
            await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch(DbUpdateException)
            {
                
                if(UserExist(customer.CustomerID))
                {
                    return Conflict();
                }
                throw;
            }

             return Ok();
 }

        // PUT: api/Customer/5
        public IHttpActionResult PutCustomer([FromUri] string id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Customer customer1 = _customerService.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            if (id != customer.CustomerID)
            {
                return BadRequest(ModelState);
            }
            customer1.ObjectState = ObjectState.Modified;
            try
            {
                _customerService.Update(customer1);
                _unitOfWork.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError(string.Empty, "there is somthing went wrong");
                if (!UserExist(id))
                {
                    return NotFound();
                }
                throw;
            }
            return Ok();
        }

        // DELETE: api/Customer/5
         [HttpDelete]
        [CustomExceptionHandler()]
        public IHttpActionResult DeleteCustomer([FromUri] string id)
        {
            Customer customer = _customerService.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            try
            { 
            customer.ObjectState = ObjectState.Deleted;
            _customerService.Delete(customer);
            _unitOfWork.SaveChanges();
            }
            catch
            { 
            //  await _unitOfWorkAsync.SaveChangesAsync();
          
                throw new Exception("some thing happen");
            }
            return StatusCode(HttpStatusCode.OK);
        }

        private bool UserExist(string Id)
        {
            return _customerService.Query(e=>e.CustomerID == Id).Select().Any();
        }
    }
}
