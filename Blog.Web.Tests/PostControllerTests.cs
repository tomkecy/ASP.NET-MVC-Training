using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using Blog.Controllers;
using Blog.Domain.Abstract;
using Blog.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject.Infrastructure.Language;

namespace Blog.Web.Tests
{
    [TestClass]
    public class PostControllerTests
    {
        [TestMethod]
        public void CanReturnProperAmountOfPosts()
        {
            //Arrange
            Mock<IPostRepository> mock = new Mock<IPostRepository>();
            mock.Setup(m => m.GetAll()).Returns((new Post[]
            {
                new Post() {Id = 1, Content = "P1"},
                new Post() {Id = 2, Content = "P2"},
                new Post() {Id = 3, Content = "P3"},
                new Post() {Id = 4, Content = "P4"},
            }).AsQueryable());

            PostController postController = new PostController(mock.Object);
            postController.NumberOfPosts = 3;

            //Act
            Post[] result = ((IEnumerable<Post>) postController.List().Model).ToArray();

            //Assert

            Assert.IsTrue(result.Length == 3);
            Assert.AreEqual(result[0].Content, "P1");
            Assert.AreEqual(result[1].Content, "P2");
            Assert.AreEqual(result[2].Content, "P3");

        }//END of CanReturnProperAmountOfPosts method 
    }//END of class PostControllerTests 
}
