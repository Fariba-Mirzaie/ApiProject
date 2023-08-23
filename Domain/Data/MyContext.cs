using Microsoft.EntityFrameworkCore;

namespace MyWebAPI.Models
{
    public class MyContext :DbContext
    {
        public MyContext(DbContextOptions<MyContext> options):base(options) 
        {
        
        }

        public DbSet<Issue1> Issues { get; set; }


    }
}
