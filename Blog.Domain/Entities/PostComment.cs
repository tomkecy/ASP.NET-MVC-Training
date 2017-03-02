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
        public String Content { get; set; }
        public DateTime CreationDateTime { get; set; }

    }//END of class PostComment

}
