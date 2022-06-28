using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ag_core
{
   public class cFollower : IFollower
    {
        FileManagement fileM = null;
        private List<User> userFollowList = new List<User>();
        public List<User> UserFollowList
        {
            get { return userFollowList; }
            set { this.userFollowList = value; }
        }

        public cFollower()
        {
            fileM = new FileManagement();
        }

        /// <summary>
        /// Populates the user list as per the user.tx file
        /// </summary>
        public void GetFollowers()
        {

           var contents = fileM.ReadFromFile("user.txt");


            foreach(var cont in contents)
            {
                UserFollowList.Add(new User()
                {
                    Username = cont.Key.Replace(" ", ""),
                    FollowerList = cont.Value.ToList()
                });
            
            }

            List<String> noFollowerList = new List<string>();

            UserFollowList.ForEach(d => d.FollowerList.ForEach(s =>
            {
                var notFound = UserFollowList.Where(a => a.Username.Contains(s.Replace(" ",""))).FirstOrDefault();

                if (notFound == null)
                {
                    noFollowerList.Add(s.Replace(" ", ""));
                }
               
            }));

            noFollowerList.ForEach(s =>
            {
                if(UserFollowList.Where(p=>p.Username == s).FirstOrDefault() == null)
                    UserFollowList.Add(new User
                    {
                        Username = s,
                        FollowerList = new List<string>()
                    });
            });

        }

        public class User
        {
            public  string Username { get; set; }
            public  int FollowerCount { get; set; }
            public  List<String> FollowerList { get; set; }
        }

    }
}
