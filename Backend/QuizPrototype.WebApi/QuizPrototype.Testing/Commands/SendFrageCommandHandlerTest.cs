using NUnit.Framework;
using QuizPrototype.Domain.Interfaces;
using QuizPrototype.WebApi.Commands;
using Moq;
using QuizPrototype.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace QuizPrototype.Testing.Commands
{
    class SendFrageCommandHandlerTest
    {
        private Mock<IFrageRepository> frageRepository;
        private Mock<IGameService> gameService;

        [SetUp]
        public void Setup()
        {
            var frage = new Frage
            {
                Id = 1,
                FrageText = "Ordnen nach Grösse",
                Auswahlmoeglichkeiten = new List<Auswahl>
                    {
                        new Auswahl { Id = 1, AuswahlText = "Test", Order=2, FrageId = 1, AssetId = "testAsset" },
                        new Auswahl { Id = 2, AuswahlText = "Test2", Order=1, FrageId = 1, AssetId = "testAsset2" }
                    }
            };

            gameService = new Mock<IGameService>();
            gameService.Setup(x => x.GetNextFrage(It.IsAny<string>()))
                .Returns(Task.FromResult(frage));

            frageRepository = new Mock<IFrageRepository>();
            frageRepository.Setup(x => x.GetCurrentFrage())
                .Returns(Task.FromResult(frage));
        }

        [Test]
        public async Task TestGetCurrentFrageFromHandlerAsync()
        {
            // Arrange
            var sut = new SendFrageCommandHandler(frageRepository.Object, gameService.Object);
            var command = new SendFrageCommand();   
            var expectedFrageId = 1;

            // Act
            var result = await sut.Handle(command, CancellationToken.None);

            // Assert
            Assert.AreEqual(expectedFrageId, result.Id);
        }

        [Test]
        public async Task TestGetNextFrageFromHandlerAsync()
        {
            // Arrange
            var sut = new SendFrageCommandHandler(frageRepository.Object, gameService.Object);
            var command = new SendFrageCommand { Guid = "3431cbe9-4918-460e-a3be-80b6017bb9d9" };
            var expectedFrageId = 1;

            // Act
            var result = await sut.Handle(command, CancellationToken.None);

            // Assert
            Assert.AreEqual(expectedFrageId, result.Id);
        }
    }
}
