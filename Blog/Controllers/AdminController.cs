using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Domain.Abstract;
using Blog.Domain.Entities;

namespace Blog.Controllers
{
    public class AdminController : Controller
    {
        #region PrivateFields

        private IPostRepository postRepository;

        #endregion

        #region Constructors

        public AdminController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ViewResult Index()
        {
            return View(postRepository.GetAll());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ViewResult Edit(int id)
        {
            Post post = postRepository.GetById(id);
            return View(post);
        }//END of Edit method

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

        [HttpPost]
        public ActionResult Delete(int id)
        {
            postRepository.Delete(id);
        }

    }//END of class AdminController
}