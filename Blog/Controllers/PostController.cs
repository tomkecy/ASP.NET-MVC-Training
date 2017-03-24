using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Blog.Domain.Abstract;
using Blog.Domain.Entities;
using Blog.Models;
using Ninject.Infrastructure.Language;

namespace Blog.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class PostController : Controller
    {
        #region PrivateFields

        private readonly IPostRepository _postRepository;
        private int _postsPerPage = 5;

        #endregion

        #region Constructors

        public PostController(IPostRepository postRepository)
        {
            this._postRepository = postRepository;
        }

        #endregion

        #region Properites

        public int PostsPerPage
        {
            get { return _postsPerPage; }
            set { _postsPerPage = value; }
        }//END of NumberOfPosts property

        #endregion

        /// <summary>
        /// HTTP GET method which returns view response containing posts list.
        /// </summary>
        /// <returns>View with posts list</returns>
        public ViewResult List(int page=1)
        {
            IEnumerable<Post> allPosts = _postRepository.GetAll().OrderByDescending(x => x.CreationDateTime);
            PostsListViewModel postsListViewModel = new PostsListViewModel()
            {
                Posts = allPosts.Skip((page - 1) * _postsPerPage).Take(_postsPerPage).ToList(),
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    PostsPerPage = _postsPerPage,
                    TotalPosts = allPosts.Count()
                }
                
            };
            return View(postsListViewModel);
        }//END of Index method

        public ViewResult Details(int id)
        {
            Post post = _postRepository.GetById(id);
            return View(post);
        }//END of Details method

    }//END of public class PostController
}