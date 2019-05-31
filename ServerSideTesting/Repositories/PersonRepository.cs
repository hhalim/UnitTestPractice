using System;
using System.Collections.Generic;
using System.Linq;
using ServerSide.Models;

namespace ServerSide.Repositories
{
    public interface IPersonRepository
    {
        
        IEnumerable<Person> findAll();
    }

    public class PersonRepository : IPersonRepository
    {
        private PersonDbContext context;

        public PersonRepository(PersonDbContext context)
        {
            if (context.People.Count() == 0)
            {
                context.Initialize();
                context.SaveChanges();
            }

            this.context = context;
        }
        public IEnumerable<Person> findAll()
        {
            return context.People.AsEnumerable();
        }

        
    }
}
