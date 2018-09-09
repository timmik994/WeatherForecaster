using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using WheatherForecaster.Models;

namespace WhetherForecaster.Models
{
    public class WheatherDbContext : DbContext
    {
        public WheatherDbContext(DbContextOptions<WheatherDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets table with whether records.
        /// </summary>
        public DbSet<WheatherRecord> WhetherRecords { get; set; }

        /// <summary>
        /// Gets or sets table with standard deviations.
        /// </summary>
        public DbSet<StandartDeviation> Deviations { get; set; }
    }
}
