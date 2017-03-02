using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Blog.Domain.Abstract;
using Blog.Domain.Entities;
using Ninject.Infrastructure.Language;

namespace Blog.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class PostController : Controller
    {
        #region PrivateFields

        private IPostRepository postRepository;
        private int numberOfPosts = 5;

        #endregion



        #region Constructors

        public PostController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        #endregion

        #region Properites

        public int NumberOfPosts
        {
            get { return numberOfPosts; }
            set { numberOfPosts = value; }
        }//END of NumberOfPosts property

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ViewResult Index()
        {
            IEnumerable<Post> posts = postRepository.GetAll().OrderBy(x => x.CreationDateTime).Take(numberOfPosts);
            return View(posts);
        }//END of Index method
    }//END of public class PostController
}