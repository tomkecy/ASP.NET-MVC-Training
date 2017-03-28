using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using Blog.Controllers;
using Blog.Domain.Abstract;
using Blog.Domain.Entities;
using Blog.Models;
using Moq;
using Ninject.Infrastructure.Language;
using NUnit.Framework;

namespace Blog.Web.Tests
{
    [TestFixture]
    public class PostControllerTests
    {
        #region Private fields

        private Mock<IPostRepository> _postRepositoryMock;

        #endregion
        #region Private methods

        private void SetupPostRepositoryMock()
        {
            _postRepositoryMock = new Mock<IPostRepository>();

            ICollection<Post> posts = new List<Post>()
            {
                new Post() {Id = 1, Content = "P1"},
                new Post() {Id = 2, Content = "P2"},
                new Post() {Id = 3, Content = "P3"},
                new Post() {Id = 4, Content = "P4"},
            };

            _postRepositoryMock.Setup(m => m.GetAll()).Returns(posts.AsQueryable());

            _postRepositoryMock.Setup(m => m.GetById(It.IsAny<int>())).Returns((int i) => posts.SingleOrDefault(p => p.Id == i));

        }
        
        #endregion

        [SetUp]
        public void TestsSetup()
        {
            SetupPostRepositoryMock();
        }

        [TearDown]
        public void TestsTearDown()
        {
            _postRepositoryMock = null;
        }

        [Test]
        public void CanReturnProperAmountOfPosts()
        {
            //Arrange
            PostController postController = new PostController(_postRepositoryMock.Object);
            postController.PostsPerPage = 3;

            //Act
            Post[] result = ((PostsListViewModel) postController.List().Model).Posts.ToArray();

            //Assert

            Assert.IsTrue(result.Length == 3);

        }//END of CanReturnProperAmountOfPosts method

        [Test]
        public void CanReturnProperPost()
        {
            //Arrange
            PostController target = new PostController(_postRepositoryMock.Object);

            //Act
            Post result = (Post) target.Details(1).Model;

            //Assert
            Assert.AreEqual(result.Id, 1);
        }
    }//END of class PostControllerTests 
}
