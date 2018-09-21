using System;
using System.Collections.Generic;
using Northwind.Entities.Models;
using Repository.Pattern.Repositories;
using Service.Pattern;
using Northwind.Repository;
using System.ComponentModel.DataAnnotations;
using Northwind.Repository.Repositories;
using Northwind.Repository.Models;

namespace Northwind.Service
{
    /// <summary>
    /// Add the custom bussiness logic(Methods) here.
    /// </summary>
    public interface IExceptionLoggerService : IService<ExceptionLogger>
    {
       // IEnumerable<ExceptionLogger> GetException(string companyName);

    }

    /// <summary>
    ///     All methods that are exposed from Repository in Service are overridable to add business logic,
    ///     business logic should be in the Service layer and not in repository for separation of concerns.
    /// </summary>
    public class ExceptionLoggerService : Service<ExceptionLogger>, IExceptionLoggerService
    {
           private readonly IRepositoryAsync<ExceptionLogger> _repository;
        public ExceptionLoggerService(IRepositoryAsync<ExceptionLogger> repository) : base(repository)
        {
            _repository = repository;   
        }
        public override void Insert(ExceptionLogger entity)
        {
            //base.Insert(entity);
            _repository.Insert(entity);
        }
    }
}
