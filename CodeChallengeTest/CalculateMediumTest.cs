using CodeChallenge_SV.BusinessLogicLayer;
using CodeChallenge_SV.Dtos;
using CodeChallenge_SV.Models;
using FluentAssertions;

namespace CodeChallengeTest
{
    public class CalculateMediumTest
    {
        [Fact]
        public void CalculateMediumWeight()
        {
            // Arrange
            string searchInput = "Max Mustermann";
            string[] searchWords = { "Max Mustermann" };
            var results = new List<SearchResult>();
            var medium = new Medium
            {
                Owner = "Max Mustermann",
                SerialNumber = "123456",
                Description = "Max Mustermanns Transponder",
                Type = "Transponder"
            };

            // Act
            SearchBll.CalculateMediumWeight(searchInput, searchWords, medium, ref results);

            // Assert
            Assert.Equal(106, results[0].Weight);
        }

        [Fact]
        public void CalculateTransitiveMediumWeight()
        {
            // Arrange
            string searchInput = "Max";
            string[] searchWords = { "Max" };
            var results = new List<SearchResult>();
            var medium = new Medium
            {
                Owner = "Max Mustermann",
                SerialNumber = "123456",
                Description = "Max Mustermanns Transponder",
                Type = "Transponder",
                Group = new Group
                {
                    Name = "Max Mustermanns Gruppe"
                }
            };

            // Act
            SearchBll.CalculateMediumWeight(searchInput, searchWords, medium, ref results);

            // Assert
            results[0].Weight.Should().Be(24);
        }

        [Fact]
        public void SetEntityTypeToMedium()
        {
            // Arrange
            string searchInput = "Max";
            string[] searchWords = { "Max" };
            var results = new List<SearchResult>();
            var medium = new Medium
            {
                Owner = "Max Mustermann",
                SerialNumber = "123456",
                Description = "Max Mustermanns Transponder",
                Type = "Transponder",
                Group = new Group
                {
                    Name = "Max Mustermanns Gruppe"
                }
            };

            // Act
            SearchBll.CalculateMediumWeight(searchInput, searchWords, medium, ref results);

            // Assert
            results[0].EntityType.Should().Be("Medium");
        }
    }
}
