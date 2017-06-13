using WebAPI.Models;
using System;
using System.Linq;

namespace WebAPI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(HimContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Persons.Any())
            {
                return;   // DB has been seeded
            }
            var persons = new Person[]
            {
            new Person{fid="3b3f1b35-12d3-4392-a848-b49419b94608",tags="sjh;monitor"},
            new Person{fid="b07e25c7-01c2-4560-b0d4-216351762e52",tags="xyc;developer" },
            new Person{fid="d69ebec5-a42b-48a6-a03e-f416044602a3",tags="xdfy;developer" }
            };
            foreach (Person s in persons)
            {
                context.Persons.Add(s);
            }
            context.SaveChanges();
        }
    }
}
