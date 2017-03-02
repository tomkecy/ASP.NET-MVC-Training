using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Blog.Controllers;
using Blog.Domain.Abstract;
using Blog.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Blog.Web.Tests
{
    [TestClass]
    public class AdminControllerTests
    {
        #region PrivateMethods
        private static Mock<IPostRepository> CreateIPostRepositoryMock()
        {
            Mock<IPostRepository> mock = new Mock<IPostRepository>();
            Post[] posts = new Post[] {
                new Post() {Id = 1, Content = "P1"},
                new Post() {Id = 2, Content = "P2"},
                new Post() {Id = 3, Content = "P3"},
                new Post() {Id = 4, Content = "P4"},
                };
            mock.Setup(x => x.GetAll()).Returns(posts.AsQueryable());
            mock.Setup(x => x.GetById(It.IsAny<int>())).Returns((int i) => posts.SingleOrDefault(x => x.Id == i));
            return mock;
        }//END of CreateIPostRepositoryMock method
        #endregion

        [TestMethod]
        public void CanReturnAllPosts()
        {
            //Arrange 
            var mock = CreateIPostRepositoryMock();

            AdminController target = new AdminController(mock.Object);

            //Act
            Post[] result = ((IEnumerable<Post>) target.Index().Model).ToArray();

            //Assert

            Assert.IsTrue(result.Length == 4);
            Assert.AreEqual(result[0].Content, "P1");
            Assert.AreEqual(result[1].Content, "P2");
            Assert.AreEqual(result[2].Content, "P3");
            Assert.AreEqual(result[3].Content, "P4");
        }//END of CanReturnAllPosts method

        [TestMethod]
        public void CanEditProduct()
        {
            //Arrange
            Mock<IPostRepository> mock = CreateIPostRepositoryMock();
            AdminController target = new AdminController(mock.Object);

            //Act
            Post p1 = target.Edit(1).Model as Post;
            Post p2 = target.Edit(2).Model as Post;
            Post p3 = target.Edit(3).Model as Post;

            //Assert
            Assert.AreEqual(1, p1.Id);
            Assert.AreEqual(2, p2.Id);
            Assert.AreEqual(3, p3.Id);

        }//END of CanEditProduct method

        [TestMethod]
        public void CannotEditNonexistentPosts()
        {
            //Arrange
            Mock<IPostRepository> mock = CreateIPostRepositoryMock();
            AdminController target = new AdminController(mock.Object);

            //Act
            Post result = target.Edit(10).Model as Post;

            //Assert
            Assert.IsNull(result);
        }//END of CannotEditNonexistentPosts method

        [TestMethod]
        public void CanSaveValidChanges()
        {
            //Arrange
            Mock<IPostRepository> mock = new Mock<IPostRepository>();
            AdminController target = new AdminController(mock.Object);
            Post post = new Post() {Id = 1, Title = "P1", Content = "P1"};

            //Act
            ActionResult result = target.Edit(post);

            //Assert
            mock.Verify(m => m.Update(post));
            
            Assert.IsNotInstanceOfType(result, typeof (ViewResult));
        }//END of CanSaveValidChanges method 

        [TestMethod]
        public void CannotSaveInvalidChanges()
        {
            //Arrange
            Mock<IPostRepository> mock = new Mock<IPostRepository>();
            AdminController target = new AdminController(mock.Object);
            target.ModelState.AddModelError("Error", "Error");
            Post post = new Post() {Id = 1, Title = "P1", Content = "P1"};

            //Act
            ActionResult result = target.Edit(post);

            //Assert
            mock.Verify(m => m.Update(It.IsAny<Post>()), Times.Never);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }//END of class AdminControllerTests
}
