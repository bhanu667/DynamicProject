
using AdminLTE1.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE1.Models
{
    public class AddDbContext : IdentityDbContext<ApplicationUser>
    {
        public AddDbContext(DbContextOptions<AddDbContext> options)
        : base(options)
        {
        }

        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<MenuPermission> MenuPermissions { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<CMSItems> CMSItems { get; set; }
        public DbSet<ApplicationUser> users { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<OrderAPI> OrderAPI { get; set; }
        public DbSet<SurveyUrl> SurveyUrl { get; set; }
        public DbSet<StateData> StateData { get; set; }
        public DbSet<CityData> CityData { get; set; }
        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Course> Course { get; set; }
    }
}
