using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Domain.Entities;

namespace Blog.Models
{
    public class CommentListViewModel
    {
        public IEnumerable<PostComment> PostComments{ get; set; }
        public PostComment NewComment { get; set; }

    }
}