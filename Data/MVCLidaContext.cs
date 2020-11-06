using Microsoft.EntityFrameworkCore;
using NTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTest.Data
{
    public class MVCLidaContext : DbContext
    {
        public MVCLidaContext(DbContextOptions<MVCLidaContext> options)
            : base(options)
        {
        }

        public DbSet<LidaData> LidaData { get; set; }
    }
}
