using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Caliburn.Micro;
using Client.Misc;
using Client.ServicesClient.Services.PostSummaries;
using Client.ViewModels;
using Models;
using Moq;
using NUnit.Framework;

namespace Client.Test
{
    [TestFixture]
    public class PostListViewModelTest
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            navigationMock = new Mock<INavigationService>();
            progressBarMock = new Mock<IProgressBarController>();
            service = new Mock<IPostSummariesService>();
            backgroundLoader = new Mock<IBackgroundLoader>();
            windowmanager = new Mock<IWindowManager>();

            service.Setup(summariesService => summariesService.GetPostSummaries()).Returns(() =>
            {
                var task =
                    new Task<IEnumerable<PostSummary>>(
                        () =>
                            new List<PostSummary>()
                            {
                                new PostSummary() { Id = 1, Title = "ABC" },
                                new PostSummary() { Id = 2, Title = "abc" },
                                new PostSummary() { Id = 3, Title = "#431" },
                                new PostSummary() { Id = 1, Title = "  777 " },
                                new PostSummary() { Id = 1, Title = "5   " },
                            });

                task.Start();
                task.Wait();
                return task;
            });
        }

        #endregion

        private Mock<INavigationService> navigationMock;

        private Mock<IProgressBarController> progressBarMock;

        private Mock<IPostSummariesService> service;

        private Mock<IBackgroundLoader> backgroundLoader;
        private Mock<IWindowManager> windowmanager;

        [TestCase]
        public void TestLoaded()
        {
            var viewModel = new PostListViewModel(navigationMock.Object,
                service.Object,
                progressBarMock.Object,
                windowmanager.Object,
                backgroundLoader.Object);

            ScreenExtensions.TryActivate(viewModel);

            progressBarMock.Verify(controller => controller.Show(), Times.Exactly(1));

            service.Verify(postSummariesService => postSummariesService.GetPostSummaries(), Times.Exactly(1));

            backgroundLoader.Verify(loader => loader.RegisterObserver(It.IsAny<IObserver<long>>()), Times.Exactly(1));
        }

        [TestCase]
        public void TestServiceUnavalaibale()
        {
            service.Setup(summariesService => summariesService.GetPostSummaries()).Returns(() =>
            {
                throw new Exception();
            });

            var viewModel = new PostListViewModel(navigationMock.Object,
                service.Object,
                progressBarMock.Object,
                windowmanager.Object,
                backgroundLoader.Object);

            ScreenExtensions.TryActivate(viewModel);

            windowmanager.Verify(manager => manager.ShowDialog(It.IsAny<DialogViewModel>(), null, null), Times.Exactly(1));
        }

        [TestCase]
        public void GetDataScenario()
        {
            var viewModel = new PostListViewModel(navigationMock.Object,
                service.Object,
                progressBarMock.Object,
                windowmanager.Object,
                backgroundLoader.Object);

            ScreenExtensions.TryActivate(viewModel);

            Assert.IsTrue(viewModel.OriginList.Count == 5);
            Assert.IsTrue(viewModel.PostList.Count == 5);
        }

        [TestCase]
        public void FilterScenario()
        {
            var viewModel = new PostListViewModel(navigationMock.Object,
                service.Object,
                progressBarMock.Object,
                windowmanager.Object,
                backgroundLoader.Object);

            ScreenExtensions.TryActivate(viewModel);

            viewModel.SearchText = "3";

            Assert.IsTrue(viewModel.OriginList.Count == 5);
            Assert.IsTrue(viewModel.PostList.Count == 1);

            viewModel.SearchText = " ";

            Assert.IsTrue(viewModel.PostList.Count == 5);

            viewModel.SearchText = string.Empty;

            Assert.IsTrue(viewModel.PostList.Count == 5);

            viewModel.SearchText = "abc";

            Assert.IsTrue(viewModel.PostList.Count == 2);

            viewModel.SearchText = "777";

            Assert.IsTrue(viewModel.PostList.Count == 1);

            viewModel.SearchText = "5";

            Assert.IsTrue(viewModel.PostList.Count == 1);
        }
    }
}
