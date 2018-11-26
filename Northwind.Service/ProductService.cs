#region

using Northwind.Entities.Models;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System.Collections;
using System.Collections.Generic;
using System;
using Northwind.Repository.Repositories;

#endregion

namespace Northwind.Service
{
    public interface IProductService : IService<Product>
    {
        IEnumerable<Product> GetProductDetails();
    }

    public class ProductService : Service<Product>, IProductService
    {
        private readonly IRepositoryAsync<Product> _repository;
        public ProductService(IRepositoryAsync<Product> repository) : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<Product> GetProductDetails()
        {
            return _repository.GetAllProduct();
        }
        public override void Insert(Product entity)
        {
            _repository.Insert(entity);
        }
    }
}