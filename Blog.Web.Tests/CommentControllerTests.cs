using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Blog.Controllers;
using Blog.Domain.Abstract;
using Blog.Domain.Entities;
using Blog.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Blog.Web.Tests
{
    /// <summary>
    /// Summary description for CommentControllerTests
    /// </summary>
    [TestClass]
    public class CommentControllerTests
    {
        [TestMethod]
        public void CanReturnProperModel()
        {
            //Arrange
            PostComment[] comments = new PostComment[]
            {
                new PostComment() {Id = 1, Content = "C1"},
                new PostComment() {Id = 2, Content = "C2"},
                new PostComment() {Id = 3, Content = "C3"},
                new PostComment() {Id = 4, Content = "C4"},
                new PostComment() {Id = 5, Content = "C5"}

            };

            Post[] posts = new Post[] {
                new Post() {Id = 1, Content = "P1", PostComments = comments},
                new Post() {Id = 2, Content = "P2", PostComments = comments},
                new Post() {Id = 3, Content = "P3", PostComments = comments},
                new Post() {Id = 4, Content = "P4", PostComments = comments},
                };

            Mock<IPostRepository> mock = new Mock<IPostRepository>();
            mock.Setup(x => x.GetById(It.IsAny<int>())).Returns((int i) => posts.SingleOrDefault(x => x.Id == i));

            CommentController target = new CommentController(mock.Object);

            //Act
            CommentListViewModel result= (CommentListViewModel) target.List(1).Model;


            //Assert
            Assert.AreEqual(result.NewComment.PostId, 1);
            Assert.AreEqual(result.PostComments, comments);
        }//END of CanReturnProperModel method
    }//END of CommentControllerTests class
}
