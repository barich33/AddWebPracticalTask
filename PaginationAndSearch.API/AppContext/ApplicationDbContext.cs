

using Microsoft.EntityFrameworkCore;
using PaginationAndSearch.API.Models;

namespace PaginationAndSearch.API.AppContext
{
   
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
