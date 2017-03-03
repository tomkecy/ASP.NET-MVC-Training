using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Domain.Abstract;
using Blog.Domain.Concrete;
using Blog.Domain.Entities;

namespace Blog.Controllers
{
    /// <summary>
    /// Provides methods that respond to HTTP requests 
    /// linked with admin panel
    /// </summary>
    public class AdminController : Controller
    {
        #region PrivateFields

        private IPostRepository postRepository;

        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postRepository"></param>
        public AdminController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }
        #endregion

        /// <summary>
        /// Returns viewresult with main admin panel
        /// </summary>
        /// <returns>View with list of all posts</returns>
        public ViewResult Index()
        {
            return View(postRepository.GetAll());
        }//END of Index method

        /// <summary>
        /// Returns View with new post creation
        /// </summary>
        /// <returns>View with new post editor</returns>
        public ViewResult Create()
        {
            return View("Edit", new Post());
        }//END of Create method

        /// <summary>
        /// Returns existing post editor view
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns>Edit ViewResult</returns>
        public ViewResult Edit(int id)
        {
            Post post = postRepository.GetById(id);
            return View(post);
        }//END of Edit method

        /// <summary>
        /// Updates post in underlying repository
        /// </summary>
        /// <param name="post">Updated post</param>
        /// <returns>Edit view</returns>
        [HttpPost]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                postRepository.Update(post);
                TempData["message"] = string.Format("Zapisano {0} ", post.Title);
                return RedirectToAction("Index");
            }
            return View(post);
        }//END of Edit method

        /// <summary>
        /// Removes post with passed id from underlying repository
        /// and returns to Index action
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns>Redirect To Action Index</returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Post post = postRepository.Delete(id);
            if (post != null)
            {
                TempData["message"] = string.Format("Usunięto {0} ", post.Title);
         
            }
            return RedirectToAction("Index");
        }//END of Delete method

    }//END of class AdminController
}