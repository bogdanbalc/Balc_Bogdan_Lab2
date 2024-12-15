using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Balc_Bogdan_Lab2.Data;
using Balc_Bogdan_Lab2.Models;

namespace Balc_Bogdan_Lab2.Pages.Books
{
    public class EditModel : BookCategoriesPageModel
    {
        private readonly Balc_Bogdan_Lab2.Data.Balc_Bogdan_Lab2Context _context;

        public EditModel(Balc_Bogdan_Lab2.Data.Balc_Bogdan_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book =  await _context.Book.Include(b => b.BookCategories).ThenInclude(b => b.Category).AsNoTracking().FirstOrDefaultAsync(m => m.ID == id);
            if (book == null)
            {
                return NotFound();
            }
            Book = book;
            PopulateAssignedCategoryData(_context, Book);
            ViewData["AuthorID"] = new SelectList(_context.Set<Author>(), "ID", "AuthorFullName");
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookToUpdate = await _context.Book
                .Include(b => b.BookCategories)
                .ThenInclude(b => b.Category)
                .FirstOrDefaultAsync(b => b.ID == id);

            if (bookToUpdate == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Attach(Book).State = EntityState.Modified;

            if (await TryUpdateModelAsync<Book>(
                bookToUpdate,
                "Book",
                b => b.Title, b => b.AuthorID, b => b.Price, b => b.PublishingDate, b => b.PublisherID))
            {
                UpdateBookCategories(_context, selectedCategories, bookToUpdate);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(bookToUpdate.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToPage("./Index");
            }

            PopulateAssignedCategoryData(_context, bookToUpdate);
            return Page();
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.ID == id);
        }
    }
}
