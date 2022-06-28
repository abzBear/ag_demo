using ag_core;
using System;
using Xunit;

namespace ag_test
{
    public class FileTests
    {

        FileManagement fileM;
        cFollower follower;
        cFeed feed;

        public FileTests()
        {
            fileM = new FileManagement();
            follower = new cFollower();
            feed = new cFeed();
        }

        [Theory]
        [InlineData("user.txt")]
        [InlineData("tweet.txt")]
        public void Initial_Data_Should_Contain_Files(string fileName)
        {
            //Act
            var result = fileM.CheckFileExist(fileName);

            //Assert
            Assert.False(result, $"{fileName} file does not exist");
        }

        [Fact]
        public void Follower_Data_Initliased()
        {
            //Act
            follower.GetFollowers();
            var result = follower.UserFollowList;

            //Assert
            Assert.False(result.Count <= 0, $"User file has no data");
        }

        [Fact]
        public void Feed_Data_Initliased()
        {
            //Act
            feed.GetFeedList();
            var result = feed.UserTweetList;

            //Assert
            Assert.False(result.Count <= 0, $"Feed file has no data");
        }
    }
}
