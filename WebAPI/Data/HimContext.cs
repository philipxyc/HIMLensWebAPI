using WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Data
{
    public class HimContext : DbContext
    {
        public HimContext(DbContextOptions<HimContext> options) : base(options)
        {
        }
        public DbSet<Person> Persons { get; set; }
    }
}
