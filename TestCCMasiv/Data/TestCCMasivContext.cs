using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestCCMasiv.Models;

namespace TestCCMasiv.Data
{
    public class TestCCMasivContext : DbContext
    {
        public TestCCMasivContext (DbContextOptions<TestCCMasivContext> options)
            : base(options)
        {
        }

        public DbSet<TestCCMasiv.Models.Bet> Bet { get; set; }
    }
}
