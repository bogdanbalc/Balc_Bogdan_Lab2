﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Balc_Bogdan_Lab2.Data;
using Balc_Bogdan_Lab2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Balc_Bogdan_Lab2.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly Balc_Bogdan_Lab2.Data.Balc_Bogdan_Lab2Context _context;

        public DetailsModel(Balc_Bogdan_Lab2.Data.Balc_Bogdan_Lab2Context context)
        {
            _context = context;
        }

        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.Include(b => b.Author).Include(b => b.Publisher).FirstOrDefaultAsync(m => m.ID == id);
            if (book == null)
            {
                return NotFound();
            }
            else
            {
                Book = book;
            }
            return Page();
        }
    }
}