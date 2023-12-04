using Cafe.Models;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Data{
    public class ApplicationDbcontext : DbContext{
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> option) : base(option){}
        public DbSet<Drink> Drink {get; set;}
        public DbSet<Cafe.Models.Account> Account { get; set; } = default!;
        public DbSet<Cafe.Models.Bill> Bill { get; set; } = default!;
       

              
    }
}