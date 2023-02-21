using CodeChallenge_SV.BusinessLogicLayer;
using CodeChallenge_SV.Dtos;
using CodeChallenge_SV.Models;

namespace CodeChallengeTest
{
    public class CalculateGroupTest
    {
        [Fact]
        public void CalculateGroupWeight()
        {
            // Arrange
            string searchInput = "TestGroup";
            string[] searchWords = { "TestGroup" };
            Group group = new Group
            { 
                Name = "TestGroup",
                Description = "TestGroup for testing"
            };

            List<SearchResult> results = new List<SearchResult>();

            // Act
            SearchBll.CalculateGroupWeight(searchInput, searchWords, group, ref results);

            // Assert
            Assert.Equal(95, results[0].Weight);
        }

        [Fact]
        public void EntityTypeSetToGroup()
        {
            // Arrange
            string searchInput = "TestGroup";
            string[] searchWords = { "TestGroup" };
            Group group = new Group
            {
                Name = "TestGroup",
                Description = "TestGroup for testing"
            };

            List<SearchResult> results = new List<SearchResult>();

            // Act
            SearchBll.CalculateGroupWeight(searchInput, searchWords, group, ref results);

            // Assert
            Assert.Equal("Group", results[0].EntityType);
        }
    }
}
