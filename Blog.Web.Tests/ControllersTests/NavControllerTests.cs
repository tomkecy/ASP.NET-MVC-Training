using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Controllers;
using Blog.Domain.Abstract;
using Blog.Domain.Entities;
using Moq;

namespace Blog.Web.Tests
{
    [TestFixture]
    public class NavControllerTests
    {
        #region Private fields

        private Mock<IPostRepository> _postRepositoryMock;
        #endregion

        #region Private methods

        private void SetupPostRepositoryMock()
        {
            _postRepositoryMock = new Mock<IPostRepository>();
            Post[] posts = {
                new Post() {Id = 1, Content = "P1", Category = "Cat1"},
                new Post() {Id = 2, Content = "P2", Category = "Cat1"},
                new Post() {Id = 3, Content = "P3", Category = "Cat2"},
                new Post() {Id = 4, Content = "P4", Category = "Cat3"},
                };
            _postRepositoryMock.Setup(x => x.GetAll()).Returns(posts.AsQueryable());
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
        public void CanReturnProperCategories()
        {
            //Arrange
            NavController target = new NavController(_postRepositoryMock.Object);

            //Act
            IEnumerable<string> result = (IEnumerable<string>) target.Menu().Model;

            //Assert
            Assert.Multiple(() =>
                {
                    Assert.IsTrue(result.Count()==3);
                    Assert.AreEqual(result.Distinct().Count(), result.Count());
                }
            );

        }
    }//END of NavControllerTests class
}
