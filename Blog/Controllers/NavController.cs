using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Domain.Abstract;

namespace Blog.Controllers
{
    public class NavController : Controller
    {
        #region Private Fields

        private readonly IPostRepository _postRepository;
        #endregion

        #region Constructors

        public NavController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        #endregion
        public PartialViewResult Menu()
        {
            IEnumerable<String> categories = _postRepository.GetAll().Select(x => x.Category).Distinct().OrderBy(x => x);
            return PartialView(categories);
        }//END of Menu method
    }//END of NavController class
}