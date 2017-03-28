using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Blog.Controllers;
using Blog.Domain.Abstract;
using Blog.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace Blog.Web.Tests
{
    [TestFixture]
    public class AdminControllerTests
    {
        #region Private Fields
        private Mock<IPostRepository> _postRepositoryMock;
        #endregion

        #region Private Methods
        private void SetUpPostRepositoryMock()
        {
            _postRepositoryMock = new Mock<IPostRepository>();
            Post[] posts = {
                new Post() {Id = 1, Content = "P1"},
                new Post() {Id = 2, Content = "P2"},
                new Post() {Id = 3, Content = "P3"},
                new Post() {Id = 4, Content = "P4"},
                };
            _postRepositoryMock.Setup(x => x.GetAll()).Returns(posts.AsQueryable());
            _postRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns((int i) => posts.SingleOrDefault(x => x.Id == i));
        }//END of CreateIPostRepositoryMock method


        #endregion


        [SetUp]
        public void TestsSetup()
        {
            SetUpPostRepositoryMock();
        }
        [TearDown]
        public void TestsTearDown()
        {
            _postRepositoryMock = null;
        }
        [Test]
        public void CanReturnAllPosts()
        {
            //Arrange 

            AdminController target = new AdminController(_postRepositoryMock.Object);

            //Act
            Post[] result = ((IEnumerable<Post>) target.Index().Model).ToArray();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.Length == 4);
                Assert.AreEqual(result[0].Content, "P1");
                Assert.AreEqual(result[1].Content, "P2");
                Assert.AreEqual(result[2].Content, "P3");
                Assert.AreEqual(result[3].Content, "P4");
            });
           
        }//END of CanReturnAllPosts method

        [Test]
        public void CanEditProduct()
        {
            //Arrange
            AdminController target = new AdminController(_postRepositoryMock.Object);

            //Act
            Post p1 = target.Edit(1).Model as Post;
            Post p2 = target.Edit(2).Model as Post;
            Post p3 = target.Edit(3).Model as Post;

            //Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, p1.Id);
                Assert.AreEqual(2, p2.Id);
                Assert.AreEqual(3, p3.Id);
            }
        );


    }//END of CanEditProduct method

        [Test]
        public void CannotEditNonexistentPosts()
        {
            //Arrange
            AdminController target = new AdminController(_postRepositoryMock.Object);

            //Act
            Post result = target.Edit(10).Model as Post;

            //Assert
            Assert.IsNull(result);
        }//END of CannotEditNonexistentPosts method

        [Test]
        public void CanSaveValidChanges()
        {
            //Arrange
            AdminController target = new AdminController(_postRepositoryMock.Object);
            Post post = new Post() {Id = 1, Title = "P1", Content = "P1"};

            //Act
            ActionResult result = target.Edit(post);

            //Assert
            _postRepositoryMock.Verify(m => m.Update(post));
            NUnit.Framework.Assert.IsNotInstanceOf(typeof (ViewResult), result);
        }//END of CanSaveValidChanges method 

        [Test]
        public void CannotSaveInvalidChanges()
        {
            //Arrange
            AdminController target = new AdminController(_postRepositoryMock.Object);
            target.ModelState.AddModelError("Error", "Error");
            Post post = new Post() {Id = 1, Title = "P1", Content = "P1"};

            //Act
            ActionResult result = target.Edit(post);

            //Assert
            _postRepositoryMock.Verify(m => m.Update(It.IsAny<Post>()), Times.Never);
            Assert.IsInstanceOf(typeof(ViewResult), result);
        }//END of CannotSaveInvalidChanges method

        [Test]
        public void CanDeleteExistingPosts()
        {
            //Arrange
            AdminController target = new AdminController(_postRepositoryMock.Object);
            Post post = _postRepositoryMock.Object.GetById(1);

            //Act
            target.Delete(post.Id);

            //Assert
            _postRepositoryMock.Verify(m => m.Delete(post.Id));


        }//END of CanDeleteExistingPosts method

        
    }//END of class AdminControllerTests
}
