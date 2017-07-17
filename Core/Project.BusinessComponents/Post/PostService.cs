using Angular4.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class PostService : IPostService
    {
        public readonly UtilitiesContext _context;
        public PostService(UtilitiesContext context)
        {
            _context = context;
        }

        public async Task AddEmptyPostAsync()
        {
           await _context.Posts.AddAsync(new Post());
           await _context.SaveChangesAsync();
        }
    }
}
