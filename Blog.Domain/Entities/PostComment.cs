using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Domain.Entities
{
    /// <summary>
    /// Represents post comment
    /// </summary>
    public class PostComment
    {
        [Key]
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; }
        public DateTime CreationDateTime { get; set; }

        public PostComment()
        {
            CreationDateTime = DateTime.Now;
        }

    }//END of class PostComment

}
