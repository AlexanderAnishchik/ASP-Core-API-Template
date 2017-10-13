using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class PostEntity
    {
        public int PostId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
    }
}