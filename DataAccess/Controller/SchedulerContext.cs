using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Controller
{
    public class SchedulerContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./Scheduler.db");
            //optionsBuilder.UseSqlite("Scheduler.db");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
