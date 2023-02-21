using CodeChallenge_SV.BusinessLogicLayer;
using CodeChallenge_SV.Dtos;
using CodeChallenge_SV.Models;

namespace CodeChallengeTest
{
    public class CalculateLockTest
    {
        [Fact]
        public void CalculateLockWeight()
        {
            // Arrange
            string searchInput = "404";
            string[] searchWords = { "404" };
            var lockEntity = new Lock
            {
                Name = "Testroom",
                Type = "Cylinder",
                SerialNumber = "123456",
                Floor = "1.OG",
                RoomNumber = "404",
                Description = "This is the lock for room 404",
            };
            var results = new List<SearchResult>();

            // Act
            SearchBll.CalculateLockWeight(searchInput, searchWords, lockEntity, ref results);

            // Assert
            Assert.Equal(66, results[0].Weight);
        }

        [Fact]
        public void CalculateLockTransitiveWeight()
        {
            // Arrange
            string searchInput = "Test";
            string[] searchWords = { "Test" };
            var lockEntity = new Lock
            {
                Name = "Testroom",
                Type = "Cylinder",
                SerialNumber = "123456",
                Floor = "1.OG",
                RoomNumber = "404",
                Description = "This is the lock for room 404",
                Building = new Building
                {
                    Name = "Testbuilding",
                    ShortCut = "TB"
                }
            };
            var results = new List<SearchResult>();

            // Act
            SearchBll.CalculateLockWeight(searchInput, searchWords, lockEntity, ref results);

            // Assert
            Assert.Equal(18, results[0].Weight);
        }

        [Fact]
        public void EntityTypeSetToLock()
        {
            // Arrange
            string searchInput = "Test";
            string[] searchWords = { "Test" };
            var lockEntity = new Lock
            {
                Name = "Testroom",
                Type = "Cylinder",
                SerialNumber = "123456",
                Floor = "1.OG",
                RoomNumber = "404",
                Description = "This is the lock for room 404",
                Building = new Building
                {
                    Name = "Testbuilding",
                    ShortCut = "TB"
                }
            };
            var results = new List<SearchResult>();

            // Act
            SearchBll.CalculateLockWeight(searchInput, searchWords, lockEntity, ref results);

            // Assert
            Assert.Equal("Lock", results[0].EntityType);
        }
    }
}
