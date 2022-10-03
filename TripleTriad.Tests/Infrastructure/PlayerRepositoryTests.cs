using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using TripleTriad.Infrastructure;
using TripleTriad.Infrastructure.Models;
using Xunit;

namespace TripleTriad.Engine.Tests.Infrastructure
{
    public class PlayerRepositoryTests
    {
        [Fact]
        public async Task PlayerRepository_IntegrationTests()
        {
            string path = @"C:/Users/JonathanRoger/AppData/LocalLow/DefaultCompany/TripleTriad_Unity/triad-integration.save";
            File.Delete(path); 
            var repository = new JsonPlayerRepository(path);
            await repository.SavePlayer(new PlayerDto()
            {
                Name = "Integration tester"
            });
            File.Exists(path).Should().BeTrue();
            var player = await repository.LoadCurrentPlayer();
            player.Name.Should().Be("Integration tester");
        }

    }
}
