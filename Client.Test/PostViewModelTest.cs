using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Caliburn.Micro;
using Client.Misc;
using Client.ServicesClient.Services.Comments;
using Client.ServicesClient.Services.Post;
using Client.ViewModels;
using Models;
using Moq;
using NUnit.Framework;

namespace Client.Test
{
    [TestFixture]
    public class PostViewModelTest
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            backgroundLoader = new Mock<IBackgroundLoader>();

            commentService = new Mock<ICommentService>();

            navigationMock = new Mock<INavigationService>();

            postService = new Mock<IPostService>();

            progressBarMock = new Mock<IProgressBarController>();

            windowmanager = new Mock<IWindowManager>();

            postService.Setup(summariesService => summariesService.GetPostById(It.IsAny<int>())).Returns(() =>
            {
                var task = new Task<Post>(() => new Post() { Title = "ABC", Body = "EFG", Author = new User() });

                task.Start();
                task.Wait();
                return task;
            });

            commentService.Setup(summariesService => summariesService.GetCommentForPost(It.IsAny<int>())).Returns(() =>
            {
                var task =
                    new Task<IEnumerable<Comment>>(
                        () =>
                            new List<Comment>()
                            {
                                new Comment() { Text = "1" },
                                new Comment() { Text = "2" },
                                new Comment() { Text = "3" },
                                new Comment() { Text = "4" },
                                new Comment() { Text = "55" },
                            });

                task.Start();
                task.Wait();
                return task;
            });
        }

        #endregion

        private Mock<IBackgroundLoader> backgroundLoader;

        private Mock<ICommentService> commentService;

        private Mock<INavigationService> navigationMock;

        private Mock<IPostService> postService;

        private Mock<IProgressBarController> progressBarMock;

        private Mock<IWindowManager> windowmanager;

        [TestCase]
        public void DataScenario()
        {
            var viewModel = new PostViewModel(commentService.Object,
                postService.Object,
                navigationMock.Object,
                windowmanager.Object,
                progressBarMock.Object,
                backgroundLoader.Object);

            viewModel.PostId = 1;

            ScreenExtensions.TryActivate(viewModel);

            Assert.IsTrue(viewModel.Post != null);
            Assert.IsTrue(viewModel.Post.Title == "ABC");
            Assert.IsTrue(viewModel.Post.Body == "EFG");
            Assert.IsTrue(viewModel.CommentList != null);
            Assert.IsTrue(viewModel.CommentList.Count == 5);
        }

        [TestCase]
        public void TestFailedCommentService()
        {
            commentService.Setup(summariesService => summariesService.GetCommentForPost(It.IsAny<int>())).Returns(() =>
            {
                throw new Exception();
            });

            var viewModel = new PostViewModel(commentService.Object,
                postService.Object,
                navigationMock.Object,
                windowmanager.Object,
                progressBarMock.Object,
                backgroundLoader.Object);

            viewModel.PostId = 1;

            ScreenExtensions.TryActivate(viewModel);

            windowmanager.Verify(manager => manager.ShowDialog(It.IsAny<DialogViewModel>(), null, null), Times.Exactly(0));
        }

        [TestCase]
        public void TestFailedPostService()
        {
            postService.Setup(summariesService => summariesService.GetPostById(It.IsAny<int>())).Returns(() =>
            {
                throw new Exception();
            });

            var viewModel = new PostViewModel(commentService.Object,
                postService.Object,
                navigationMock.Object,
                windowmanager.Object,
                progressBarMock.Object,
                backgroundLoader.Object);

            viewModel.PostId = 1;

            ScreenExtensions.TryActivate(viewModel);

            progressBarMock.Verify(controller => controller.Show(), Times.Exactly(1));

            windowmanager.Verify(manager => manager.ShowDialog(It.IsAny<DialogViewModel>(), null, null), Times.Exactly(1));
        }

        [TestCase]
        public void TestLoading()
        {
            var viewModel = new PostViewModel(commentService.Object,
                postService.Object,
                navigationMock.Object,
                windowmanager.Object,
                progressBarMock.Object,
                backgroundLoader.Object);

            viewModel.PostId = 1;

            ScreenExtensions.TryActivate(viewModel);

            progressBarMock.Verify(controller => controller.Show(), Times.Exactly(1));

            postService.Verify(postSummariesService => postSummariesService.GetPostById(It.IsAny<int>()), Times.Exactly(1));

            commentService.Verify(service => service.GetCommentForPost(It.IsAny<int>()), Times.Exactly(1));

            backgroundLoader.Verify(loader => loader.RegisterObserver(It.IsAny<IObserver<long>>()), Times.Exactly(1));
        }
    }
}
