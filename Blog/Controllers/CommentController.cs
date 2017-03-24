using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Domain.Abstract;
using Blog.Domain.Entities;
using Blog.Models;

namespace Blog.Controllers
{
    public class CommentController : Controller
    {
        #region Private Fields

        private IPostRepository postRepository;
#endregion
        #region Constructors
        public CommentController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }
#endregion
        public PartialViewResult List(int id)
        {
            CommentListViewModel model = new CommentListViewModel()
            {
                NewComment = new PostComment() {PostId = id},
                PostComments = postRepository.GetById(id).PostComments
            };
            return PartialView(model);
        }//END of List method

        [HttpPost]
        public RedirectToRouteResult AddComment(PostComment postComment)
        {
                postComment.CreationDateTime = DateTime.Now;
                Post post = postRepository.GetById(postComment.PostId);
                post.PostComments.Add(postComment);
                postRepository.Update(post);
                TempData["message"] = "Dodano komentarz";

            return RedirectToAction("Details", "Post", new {post.Id});
        }//END of AddComment method
    }//END of CommentController class
}