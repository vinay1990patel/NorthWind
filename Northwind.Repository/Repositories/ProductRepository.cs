using System.Collections.Generic;
using System.Linq;
using Northwind.Entities.Models;
using Northwind.Repository.Models;
using Repository.Pattern.Repositories;
namespace Northwind.Repository.Repositories
{
  public static  class ProductRepository
    {
        public static IEnumerable<Product>GetAllProduct(this IRepository<Product> repository)
        {
            var r = repository.GetRepository<Product>().Queryable();
            return r.AsEnumerable();
        }
    }
}
