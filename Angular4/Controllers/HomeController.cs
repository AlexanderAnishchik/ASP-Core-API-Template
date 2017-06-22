using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Angular4.Models;

namespace Angular4.Controllers
{
    public class HomeController : Controller
    {
        private readonly UtilitiesContext _context;
        public HomeController(UtilitiesContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
           var res = await _context.Posts.AddAsync(new Post());
                _context.SaveChanges();
                return View();
        }
    }
}