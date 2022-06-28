using ag_core;
using System;
using System.Linq;

namespace ag_tweet
{
    class Program
    {
        private static string initStatus = "", nextSteps = "";
   
        static void Main(string[] args)
        {
            bool ProgramIsRunning = true;
            cFollower follower = new cFollower();
            cFeed feed = new cFeed();
            try
            {
                //initialising the system to setup the configuration
                while (String.IsNullOrEmpty(initStatus) || initStatus.ToLower().Equals("n") || initStatus.ToLower().Equals("no"))
                {
                    initStatus = Init();
                }

                //validating files to see if they exist
                ValidateFiles();

                follower.GetFollowers();
                feed.GetFeedList();

                while (ProgramIsRunning)
                {
                    var nextOption = NextSteps();

                    switch (nextOption)
                    {
                        case "1": //option 1: Display User list
                           

                            Console.WriteLine("------------------------------------------------------");
                            follower.UserFollowList.ForEach(d =>
                            {

                                if(d.FollowerList.Count()>0)
                                    Console.WriteLine(d.Username + " follows " + String.Join(", ", d.FollowerList.Distinct()));
                                else
                                    Console.WriteLine(d.Username + " follows nobody");

                            
                            }

                            );
                            Console.WriteLine("------------------------------------------------------");

                            break;
                        case "2": //option 2: Display Feed list



                            Console.WriteLine("------------------------------------------------------");
                            feed.UserTweetList.ForEach(d =>
                            {
                                d.Message.ForEach(c =>
                                {
                                    Console.WriteLine("@" + d.Username + " : " + c);
                                });
                               
                            }

                            );
                            Console.WriteLine("------------------------------------------------------");


                            break;
                        case "3":  //option 3: Display Full feed list

                            Console.WriteLine("------------------------------------------------------");
                            follower.UserFollowList.OrderBy(q => q.Username).ToList().ForEach(d =>
                            {
                                Console.WriteLine(d.Username);

                                //finding tweets of the main user
                                feed.UserTweetList.Where(x => x.Username == d.Username).ToList().ForEach(x =>
                                 {
                                     x.Message.ForEach(d =>
                                     {
                                         //only allowed to display 140 characters
                                         Console.WriteLine("\t@" + x.Username + " : " + d.Substring(0, 140));
                                     });

                                 });

                                //finding tweets of the followers

                                d.FollowerList.ForEach(x =>
                                {
                                    var follT = feed.UserTweetList.Where(a => a.Username == x.Replace(" ", "")).ToList();
                                  follT.ForEach(t =>
                                    {

                                        t.Message.ForEach(d =>
                                        {
                                            //only allowed to display 140 characters
                                            Console.WriteLine("\t@" + t.Username + " : " + d.Substring(0,140));
                                        });
                                    });

                                });
                            

                            });

                        

                            Console.WriteLine("------------------------------------------------------");

                            break;
                        case "e":
                            ProgramIsRunning = false;
                            break;
                    }
                }
               

            }
            catch(Exception ex)
            {
                Console.WriteLine("ERROR: "+ ex.Message);
                Init();
            }
        }

        /// <summary>
        /// Initializes the program
        /// </summary>
        /// <returns></returns>
        private static string Init()
        {
            if (!String.IsNullOrEmpty(initStatus) && !initStatus.Equals("y")|| !String.IsNullOrEmpty(initStatus) && !initStatus.Equals("yes"))
            {
                Console.WriteLine("I don't recognise this option, try again!");
            }
            else
            {
                Console.WriteLine("Welcome, before we begin let's do some housework!");
                Console.WriteLine("Place the following files in the folder named Data! (Ensure the file name is spelt exactly as below)");
                Console.WriteLine("File 1: user.txt");
                Console.WriteLine("File 2: tweet.txt");
                Console.WriteLine("press (e) to exit at any time");
            }
     
            return initStatus = Console.ReadLine();
        }

        /// <summary>
        /// displays the enxt options to be chosen from
        /// </summary>
        /// <returns></returns>
        private static string NextSteps()
        {
            Console.WriteLine("Data Loaded successfully, choose option? (Enter number)");
            Console.WriteLine("#1 - Display Users");
            Console.WriteLine("#2 - Display Tweets");
            Console.WriteLine("#3 - Display Tweets per User");
            Console.WriteLine("press (e) to exit at any time");

            return nextSteps = Console.ReadLine();
        }

        private static void ValidateFiles()
        {
            FileManagement fileM = new FileManagement();
            //reading user.txt file
            if (!fileM.CheckFileExist("user.txt"))
                throw new Exception("user.txt - not found");
            //reading tweet.txt file
            if (!fileM.CheckFileExist("tweet.txt"))
                throw new Exception("tweet.txt - not found");



            Console.WriteLine("Files found successfully!");

        }
    }
}
