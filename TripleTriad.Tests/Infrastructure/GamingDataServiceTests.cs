using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using TripleTriad.Infrastructure;
using Xunit;

namespace TripleTriad.Engine.Tests.Infrastructure
{
    public class GamingDataServiceTests
    {
        [Fact]
        public async Task GamingDataService_GetDataFromAzureTable()
        {
            var sut = new GamingDataService("https://gamingapi.azurewebsites.net", "tripletriad", "1a31b626-a271-4f31-a862-66dfa1ae0077");
            var result = await sut.LoadData();
            result.Should().NotBeNull(); 
        }
        
        [Fact]
        public async Task GamingDataService_GetAllCards()
        {
            var sut = new GamingDataService("https://gamingapi.azurewebsites.net", "tripletriad", "1a31b626-a271-4f31-a862-66dfa1ae0077");
            var result = await sut.LoadAllCards();
            result.Should().NotBeNull(); 
        }
        
        [Fact]
        public async Task GamingDataService_PostDataFromAzureTable()
        {
            var sut = new GamingDataService("https://localhost:7229", "integrationtests", "INTEGRATION");
            await sut.SaveData(new Dictionary<int, string>()
            {
                { 1, "{serializedData}"}
            });
        }
    }
}