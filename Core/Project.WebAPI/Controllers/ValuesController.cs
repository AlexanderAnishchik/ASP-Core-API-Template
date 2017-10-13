using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Entities;
using BusinessComponents.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            postService = _postService;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public PostEntity Get(string id)
        {
            return new PostEntity();
        }
        // POST api/values
        [HttpPost]
        [Produces(typeof(PostEntity))]
        public async Task<IActionResult> Post([FromBody]PostEntity value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _postService.AddEmptyPostAsync();
            return CreatedAtAction("Get", new { id = 5 }, value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
