using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Win32;

namespace Blog.Domain.Entities
{
    /// <summary>
    /// Represents blog post
    /// </summary>
    public class Post
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDateTime { get; set; }
        public IEnumerable<PostComment> Comments { get; set; }

        public Post()
        {
            CreationDateTime = DateTime.Now;
        }

    }//END of class Post
}
