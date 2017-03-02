using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Domain.Abstract;

namespace Blog.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class PostController : Controller
    {
        #region Fields

        private IPostRepository postRepository;
        

        #endregion

        #region Constructors

        public PostController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        #endregion


        // GET: Post
        public ActionResult Index()
        {
            return View();
        }
    }//END of public class PostController
}