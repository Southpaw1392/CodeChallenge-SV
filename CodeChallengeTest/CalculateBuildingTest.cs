using CodeChallenge_SV.BusinessLogicLayer;
using CodeChallenge_SV.Dtos;
using CodeChallenge_SV.Models;

namespace CodeChallengeTest
{
    public class CalculateBuildingTest
    {
        [Fact]
        public void CalculateBuildingWeight()
        {
            // Arrange
            string searchInput = "Test Office";
            string[] searchWords = new string[] { "Test", "Office" };
            Building building = new Building
            {
                ShortCut = "Test",
                Name = "Test Office",
                Description = "Test Office, Test Street 1, 12345 Testtown"
            };
            List<SearchResult> results = new List<SearchResult>();

            // Act
            SearchBll.CalculateBuildingWeight(searchInput, searchWords, building, ref results);

            // Assert
            Assert.Equal(102, results[0].Weight);
        }

        [Fact]
        public void EntityTypeSetToBuilding()
        {
            // Arrange
            string searchInput = "Test Office";
            string[] searchWords = new string[] { "Test", "Office" };
            Building building = new Building
            {
                ShortCut = "Test",
                Name = "Test Office",
                Description = "Test Office, Test Street 1, 12345 Testtown"
            };
            List<SearchResult> results = new List<SearchResult>();

            // Act
            SearchBll.CalculateBuildingWeight(searchInput, searchWords, building, ref results);

            // Assert
            Assert.Equal("Building", results[0].EntityType);
        }
    }
}