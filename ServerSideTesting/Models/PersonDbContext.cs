using System;
using Microsoft.EntityFrameworkCore;

namespace ServerSide.Models
{
    public class PersonDbContext : DbContext
    {
        public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options) { }

        public virtual DbSet<Person> People { get; set; }

        public void Initialize()
        {
            var person1 = new Person()
            {
                Id = 1,
                address = "123 Main Street",
                firstName = "John",
                lastName = "Doe"
            };
            var person2 = new Person()
            {
                Id = 2,
                address = "700 Elm Ave",
                firstName = "Jane",
                lastName = "Doe"
            };
            this.People.Add(person1);
            this.People.Add(person2);
        }


    }
}
