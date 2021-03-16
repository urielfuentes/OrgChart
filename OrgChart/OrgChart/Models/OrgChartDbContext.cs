﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrgChart.Models
{
    public class OrgChartDbContext : DbContext
    {
        public OrgChartDbContext(DbContextOptions<OrgChartDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }
}