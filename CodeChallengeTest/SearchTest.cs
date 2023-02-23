using CodeChallenge_SV.BusinessLogicLayer;
using CodeChallenge_SV.DataAccessLayerInteraces;
using CodeChallenge_SV.Dtos;
using CodeChallenge_SV.Models;
using FluentAssertions;
using Moq;

namespace CodeChallengeTest
{
    public class SearchTest
    {
        private ISearchDal getMockSearchDAL() 
        {
            Mock<ISearchDal> mockObject = new Mock<ISearchDal>();
            mockObject.Setup(m => m.GetBuildingsBySearchInput(new string[] { "Test" })).Returns(Task.FromResult(new List<Building>()));
            mockObject.Setup(m => m.GetGroupsBySearchInput(new string[] { "Test" })).Returns(Task.FromResult(new List<Group>()));
            mockObject.Setup(m => m.GetLocksBySearchInput(new string[] { "Test" })).Returns(Task.FromResult(new List<Lock>()));
            mockObject.Setup(m => m.GetMediaBySearchInput(new string[] { "Test" })).Returns(Task.FromResult(new List<Medium>()));
            return mockObject.Object;
        }

        [Fact]
        public async Task FilterAndSortData()
        {
            ISearchDal searchDal = getMockSearchDAL();
            SearchBll searchBll = new SearchBll(searchDal);

            var searchInput = "Test";
            var searchWords = new string[] { "Test" };
            var buildings = new List<Building>()
            {
                new Building {
                    ShortCut = "Test",
                    Name = "Test Office",
                    Description = "Test Office, Test Street 1, 12345 Testtown"
                },
                new Building {
                    ShortCut = "HQ",
                    Name = "Head Quarters",
                    Description = "Head Quarters, HQ Street 7, 12345 Testtown"
                },
            };

            var groups = new List<Group>()
            {
                new Group {
                    Name = "Blue",
                    Description = "Members are required to wear a blue shirt"
                },
                new Group {
                    Name = "Red",
                    Description = "Members are required to wear a red shirt"
                },
            };

            var locks = new List<Lock>()
            {
                new Lock
                {
                    Name = "Testroom",
                    Type = "Cylinder",
                    SerialNumber = "123456",
                    Floor = "1.OG",
                    RoomNumber = "404",
                    Description = "This is the lock for room 404",
                    Building = new Building
                    {
                        ShortCut = "HQ",
                        Name = "Head Quarters",
                    }
                },
                new Lock
                {
                    Name = "Toilets",
                    Type = "Cylinder",
                    SerialNumber = "654321",
                    Floor = "2.OG",
                    RoomNumber = "42",
                    Description = null
                }
            };

            var media = new List<Medium>()
            {
                new Medium {
                    Owner = "Max Mustermann",
                    SerialNumber = "123456",
                    Description = "Max Mustermanns Transponder",
                    Type = "Transponder",
                    Group = new Group
                    {
                        Name = "Max Mustermanns Gruppe"
                    }
                },
                new Medium 
                {
                    Owner = "Olaf Olafson",
                    SerialNumber = "654321",
                    Description = null,
                    Type = "Transponder"
                },
            };

            var expected = new List<SearchResult>()
            {
                new SearchResult
                {
                    Weight = 84,
                    EntityType = "Building",
                    Name = "Test Office",
                    Description = "Test Office, Test Street 1, 12345 Testtown",
                    ShortCut = "Test"
                },
                new SearchResult
                {
                    Weight = 10,
                    Name = "Testroom",
                    EntityType = "Lock",
                    Type = "Cylinder",
                    SerialNumber = "123456",
                    Floor = "1.OG",
                    RoomNumber = "404",
                    Description = "This is the lock for room 404"
                },
                new SearchResult
                {
                    Weight = 5,
                    EntityType = "Building",
                    Name = "Head Quarters",
                    ShortCut = "HQ",
                    Description = "Head Quarters, HQ Street 7, 12345 Testtown"
                },
            };

            // Act
            List<SearchResult> result = await searchBll.FilterAndSortData(searchInput, searchWords, buildings, groups, locks, media);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }
    }
}
