using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ag_core
{
   public class cFeed : IFeed
    { 

        FileManagement fileM = null;
        private List<Tweet> userTweetList = new List<Tweet>();
        public List<Tweet> UserTweetList
        {
            get { return userTweetList; }
            set { this.userTweetList = value; }
        }

        public cFeed()
        {
            fileM = new FileManagement();
        }

        /// <summary>
        /// populates the feed bases on the twitter.tx content
        /// </summary>
        public void GetFeedList()
        {
            var contents = fileM.ReadFromFile("tweet.txt");


            foreach (var cont in contents)
            {
                UserTweetList.Add(new Tweet()
                {
                    Username = cont.Key.Replace(" ", ""),
                    Message = cont.Value.ToList()
                });
            }
        }

        public void FindUserFeed(string username)
        {
            throw new NotImplementedException();
        }

        public class Tweet
        {
            public string Username { get; set; }
            public List<string> Message { get; set; }
        }
      
    }
}
