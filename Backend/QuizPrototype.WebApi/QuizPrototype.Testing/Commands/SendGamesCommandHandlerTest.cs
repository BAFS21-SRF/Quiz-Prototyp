using Moq;
using NUnit.Framework;
using QuizPrototype.Domain.Entities;
using QuizPrototype.Domain.Interfaces;
using QuizPrototype.WebApi.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuizPrototype.Testing.Commands
{
    class SendGamesCommandHandlerTest
    {
        private Mock<IGameRepository> gameRepository;

        [SetUp]
        public void Setup()
        {
            var gameList = new List<Game>();
            gameList.Add(new Game { AktuelleFrageId = 1, Guid = "test", Id = 1, Score = 100 });

            gameRepository = new Mock<IGameRepository>();
            gameRepository.Setup(x => x.GetAll())
                .Returns(Task.FromResult(gameList));
        }

        [Test]
        public async Task TestGetCurrentFrageFromHandlerAsync()
        {
            // Arrange
            var sut = new SendGamesCommandHandler(gameRepository.Object);
            var command = new SendGamesCommand();

            // Act
            var result = await sut.Handle(command, CancellationToken.None);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(100, result[0].Score);
        }
    }
}
