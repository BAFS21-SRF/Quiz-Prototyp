using NUnit.Framework;
using QuizPrototype.Domain.Interfaces;
using QuizPrototype.WebApi.Commands;
using Moq;
using QuizPrototype.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace QuizPrototype.Testing.Commands
{
    class SendGameStartCommandHandlerTest
    {
        private Mock<IGameService> gameService;

        [SetUp]
        public void Setup()
        {
            gameService = new Mock<IGameService>();
            gameService.Setup(x => x.StartGame())
                .Returns(Task.FromResult(new Game { Id = 1, Guid = "fe4d4eb4-18eb-4eb2-ba15-78c4c559d661", AktuelleFrageId = 0 }));
        }

        [Test]
        public async Task TestStartGameFromHandler()
        {
            // Arrange
            var sut = new SendGameStartCommandHandler(gameService.Object);
            var command = new SendGameStartCommand();
            var expectedGameId = 1;

            // Act
            var result = await sut.Handle(command, CancellationToken.None);

            // Assert
            Assert.AreEqual(expectedGameId, result.Id);
        }
    }
}
