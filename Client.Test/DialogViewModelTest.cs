using System;
using Client.ViewModels;
using Moq;
using NUnit.Framework;

namespace Client.Test
{
    [TestFixture]
    public class DialogViewModelTest
    {
        [TestCase]
        public void TestDialog()
        {
            var action = new Mock<Action>();

            var viewModel = new DialogViewModel() { CallbackAction = action.Object };

            var emptyObject = new object();

            viewModel.DismissCommand.Execute(emptyObject);

            action.Verify(x => x(), Times.Exactly(1));
        }
    }
}
