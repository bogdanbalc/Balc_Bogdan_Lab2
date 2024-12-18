﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Balc_Bogdan_Lab2.Models;

namespace Balc_Bogdan_Lab2.Data
{
    public class Balc_Bogdan_Lab2Context : DbContext
    {
        public Balc_Bogdan_Lab2Context (DbContextOptions<Balc_Bogdan_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Balc_Bogdan_Lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<Balc_Bogdan_Lab2.Models.Publisher> Publisher { get; set; } = default!;
        public DbSet<Balc_Bogdan_Lab2.Models.Author> Author { get; set; } = default!;
    }
}
