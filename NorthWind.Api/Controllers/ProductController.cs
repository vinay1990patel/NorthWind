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
using System.Web.Http;
namespace NorthWind.Api.Controllers
{


    public class ProductController : ApiController
    {
        private IProductService _productService;
        private IUnitOfWork _unitOfWork;
        private IUnitOfWorkAsync _unitOfWorkAsync;

        public ProductController(IProductService productService, IUnitOfWork unitOfWork, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _productService = productService;
            _unitOfWork = unitOfWork;
            _unitOfWorkAsync = unitOfWorkAsync;
        }
        // GET: api/Product
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            if (!ModelState.IsValid)
            {
                Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "500");
            }
            return _productService.GetProductDetails();
        }
        // POST: api/Product
        [System.Web.Http.Description.ResponseType(typeof(Product))]
        [HttpPost]
        public async Task<IHttpActionResult> PostProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            product.ObjectState = ObjectState.Added;
            _productService.Insert(product);
            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch(DbUpdateException)
            {
                if(UserExist(product.ProductID))
                {
                    return Conflict();
                }
                throw;
            }
            return Ok();
        }
        private bool UserExist(int Id)
        {
            return _productService.Query(e => e.ProductID == Id).Select().Any();
        }
    }
}
