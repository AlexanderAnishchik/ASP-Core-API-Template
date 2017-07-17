
using DataAccess.Models;
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
    }
}
