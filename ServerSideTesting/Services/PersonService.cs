using System;
using System.Collections.Generic;
using ServerSide.Exceptions;
using ServerSide.Models;
using ServerSide.Repositories;

namespace ServerSide.Services
{
    public interface IPersonService
    {
        IEnumerable<Person> FindAll();
        Person Get(int id);
    }

    public class PersonRepository : IPersonService
    {

        private IPersonRepository _repository;

        public PersonRepository(IPersonRepository _repository)
        {
            this._repository = _repository;
        }


        public IEnumerable<Person> FindAll()
        {
            return this._repository.FindAll();
        }

        public Person Get(int id)
        {
            return this._repository.Get(id);
        }
    }
}
