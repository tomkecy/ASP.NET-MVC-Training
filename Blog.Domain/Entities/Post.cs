using System;
using System.CodeDom;
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
        [Required, MinLength(3)]
        public string Title { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime CreationDateTime { get; set; }
        public virtual ICollection<PostComment> PostComments { get; set; }

        /// <summary>
        /// Initializes new instance of Post model
        /// </summary>
        public Post()
        {
            CreationDateTime = DateTime.Now;
            PostComments = new List<PostComment>();
        }

    }//END of class Post
}
