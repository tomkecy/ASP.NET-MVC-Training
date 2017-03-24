using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class PagingInfo
    {
        public int CurrentPage { get; set; }
        public int PostsPerPage { get; set; }
        public int TotalPosts { get; set; }
        public int TotalPages => (int) Math.Ceiling((decimal) TotalPosts/PostsPerPage);
    }
}