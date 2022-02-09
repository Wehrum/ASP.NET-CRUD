using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebAppProject.Models;

namespace WebAppProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WebAppProject.Models.AssemblyInfo> AssemblyInfo { get; set; }
        public DbSet<WebAppProject.Models.Joke> Joke { get; set; }
        public DbSet<WebAppProject.Models.ProjectRole> Admin { get; set; }
        public DbSet<WebAppProject.Models.ClickIt> ClickIt { get; set; }
    }
}
