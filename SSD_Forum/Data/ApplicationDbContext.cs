using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SSD_Forum.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSD_Forum.Data
{
    public class ApplicationDbContext : IdentityDbContext <ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        //Database Tables
        public DbSet<Thread> Threads { get; set; }
        public DbSet<Reply> Replies { get; set; }

    }
}
