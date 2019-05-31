using System;
using System.Collections.Generic;
using ServerSide.Exceptions;
using ServerSide.Models;
using ServerSide.Repositories;

namespace ServerSide.Services
{
    public interface IPersonService
    {
        IEnumerable<Person> findAll();
    }

    public class PersonService : IPersonService
    {

        private IPersonRepository _repository;

        public PersonService(IPersonRepository _repository)
        {
            this._repository = _repository;
        }


        public IEnumerable<Person> findAll()
        {
            return this._repository.findAll();
        }

    }
}
