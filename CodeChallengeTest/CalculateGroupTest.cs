using CodeChallenge_SV.BusinessLogicLayer;
using CodeChallenge_SV.Dtos;
using CodeChallenge_SV.Models;
using FluentAssertions;

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
            results[0].Weight.Should().Be(95);
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
            results[0].EntityType.Should().Be("Group");
        }
    }
}
