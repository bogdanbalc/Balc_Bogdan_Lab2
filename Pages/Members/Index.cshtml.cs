using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Balc_Bogdan_Lab2.Data;
using Balc_Bogdan_Lab2.Models;

namespace Balc_Bogdan_Lab2.Pages.Members
{
    public class IndexModel : PageModel
    {
        private readonly Balc_Bogdan_Lab2.Data.Balc_Bogdan_Lab2Context _context;

        public IndexModel(Balc_Bogdan_Lab2.Data.Balc_Bogdan_Lab2Context context)
        {
            _context = context;
        }

        public IList<Member> Member { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Member = await _context.Member.ToListAsync();
        }
    }
}
