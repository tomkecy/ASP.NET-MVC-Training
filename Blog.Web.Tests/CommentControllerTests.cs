using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Blog.Controllers;
using Blog.Domain.Abstract;
using Blog.Domain.Entities;
using Blog.Models;
using Moq;
using NUnit.Framework;

namespace Blog.Web.Tests
{
    /// <summary>
    /// Tests for CommentController class.
    /// </summary>
    [TestFixture]
    public class CommentControllerTests
    {
        #region Private Fields

        private Mock<IPostRepository> _postRepositoryMock;
        #endregion
        #region Private Methods
        private void SetupPostRepositoryMock()
        {
            _postRepositoryMock = new Mock<IPostRepository>();

            ICollection<PostComment> comments = new List<PostComment>()
            {
                new PostComment() {Id = 1, Content = "C1"},
                new PostComment() {Id = 2, Content = "C2"},
                new PostComment() {Id = 3, Content = "C3"},
                new PostComment() {Id = 4, Content = "C4"},
                new PostComment() {Id = 5, Content = "C5"}
            };

            ICollection<Post> posts = new List<Post>()
            {
                new Post() {Id = 1, Content = "P1", PostComments = comments},
                new Post() {Id = 2, Content = "P2", PostComments = comments},
                new Post() {Id = 3, Content = "P3", PostComments = comments},
                new Post() {Id = 4, Content = "P4", PostComments = comments},
            };
            _postRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns((int i) => posts.SingleOrDefault(x => x.Id == i));

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
        public void CanReturnProperModel()
        {
            //Arrange  
            CommentController target = new CommentController(_postRepositoryMock.Object);

            //Act
            CommentListViewModel result = (CommentListViewModel) target.List(1).Model;


            //Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(result.NewComment.PostId, 1);
                Assert.True(result.PostComments.Count()==5);
            } 
            );

        }//END of CanReturnProperModel method

        [Test]
        public void CanAddValidComment()
        {
            //Arrange
            CommentController target = new CommentController(_postRepositoryMock.Object);
            PostComment postComment = new PostComment()
            {
                Content = "Test",
                Id = 6,
                PostId = 2
            };

            //Act
            target.AddComment(postComment);

            //Assert
            _postRepositoryMock.Verify(m => m.Update(It.IsAny<Post>()), Times.Once);
        }

        [Test]
        public void CannotAddInvalidComment()
        {
            //Arrange
            CommentController target = new CommentController(_postRepositoryMock.Object);
            target.ModelState.AddModelError("Error", "Error");
            PostComment postComment = new PostComment();

            //Act
            target.AddComment(postComment);

            //Assert
            _postRepositoryMock.Verify(m=>m.Update(It.IsAny<Post>()), Times.Never);

        }
    }//END of CommentControllerTests class
}
