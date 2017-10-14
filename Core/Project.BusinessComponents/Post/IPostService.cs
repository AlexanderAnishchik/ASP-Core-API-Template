using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessComponents.Services
{
    public interface IPostService
    {
         Task AddEmptyPostAsync();
        Task<List<Post>> GetPostsAsync();
    }
}
