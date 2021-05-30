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
    class SendNextFrageCommandHandlerTest
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
            frageRepository.Setup(x => x.GetById(It.Is<long>(x => x == 1 )))
                .Returns(Task.FromResult(frage));
        }

        [Test]
        public async Task ValidFrageId()
        {
            // Arrange
            var sut = new SendNextFrageCommandHandler(frageRepository.Object, gameService.Object);
            var command = new SendNextFrageCommand();
            var expectedFrageId = 1;
            var guid = Guid.NewGuid().ToString();
            var score = 100;
            command.FrageId = expectedFrageId;
            command.Guid = guid;
            command.Score = score;

            // Act
            var result = await sut.Handle(command, CancellationToken.None);

            // Assert
            Assert.AreEqual(expectedFrageId, result.Id);
        }

        [Test]
        public async Task NotVaildFrageId()
        {
            // Arrange
            var sut = new SendNextFrageCommandHandler(frageRepository.Object, gameService.Object);
            var command = new SendNextFrageCommand();
            var expectedFrageId = 2;
            var guid = Guid.NewGuid().ToString();
            var score = 100;
            command.FrageId = expectedFrageId;
            command.Guid = guid;
            command.Score = score;

            // Act
            var result = await sut.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNull(result);
        }
    }
}
