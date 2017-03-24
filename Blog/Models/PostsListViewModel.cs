using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Domain.Entities;

namespace Blog.Models
{
    public class PostsListViewModel
    {
        public PagingInfo PagingInfo { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}