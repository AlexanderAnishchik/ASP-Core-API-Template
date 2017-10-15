
using Domain.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessComponents.Services
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

        public Task<List<Post>> GetPostsAsync()
        {
            return Task.FromResult<List<Post>>(_context.Posts.ToList());
        }
    }
}
