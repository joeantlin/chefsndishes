using Microsoft.EntityFrameworkCore;

namespace chefsnDishes.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }
        
        // "users" table is represented by this DbSet "Users"
        public DbSet<Dish> Dishes {get;set;}
        public DbSet<Chef> Chefs {get;set;}
    }
}