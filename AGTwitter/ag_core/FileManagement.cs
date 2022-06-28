using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ag_core
{
   public class FileManagement : IFileManagement
    {

        private string projectPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\"));
        public bool CheckFileExist(string fileName)
        {

            return File.Exists(projectPath + "/Data/"+ fileName);
        }

        /// <summary>
        /// fetches all the data from the specific file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public Dictionary<string, HashSet<string>> ReadFromFile(string fileName)
        {
            Dictionary<string, HashSet<string>> content = new Dictionary<string, HashSet<string>>();
            string textLine;
            try
            {
                using (StreamReader sr = new StreamReader(projectPath + "/Data/" + fileName))
                {
                    while ((textLine = sr.ReadLine()) != null)
                    {
                        switch (fileName)
                        {
                            case "user.txt":
                                PopulateContents(textLine,"user", ref content);
                                break;

                            case "tweet.txt":
                                PopulateContents(textLine,"tweet", ref content);
                                break;

                        }
                    }
                }
            }
            catch (FileNotFoundException fnf)
            {                
                throw new Exception("You need to place the ");
            }
            catch (Exception e)
            {
                new Exception("Something went wrong: " + e);
            }

            return content;
        }

        /// <summary>
        /// finds the content of each line item in the file and modifies it
        /// </summary>
        /// <param name="textLine"></param>
        /// <param name="type"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        private Dictionary<string, HashSet<string>> PopulateContents(string textLine,string type, ref Dictionary<string, HashSet<string>> content)
        {

          
            switch (type)
            {
                case "user":

                    FillHashContext(textLine, "follows", type,ref content);
                    break;
                case "tweet":

                    FillHashContext(textLine, ">", type,ref content);

                    break;
            }
            return content;
        }

        /// <summary>
        /// Populates the data according to the content of the file
        /// </summary>
        /// <param name="textLine"></param>
        /// <param name="delimeter"></param>
        /// <param name="type"></param>
        /// <param name="content"></param>
        private void FillHashContext(string textLine, string delimeter, string type, ref Dictionary<string, HashSet<string>> content)
        {

            HashSet<string> item;

            string user = textLine.Substring(0, textLine.IndexOf(delimeter)).Replace(" ","");
            string followMsg = textLine.Substring(textLine.IndexOf(delimeter) + delimeter.Length);

            if (!content.ContainsKey(user))
            {
                item = new HashSet<string>();
                content.Add(user, item);
            }
            else
            {
                item = content[user];
            }

            if(type == "user")
            {
                var nameList = followMsg.Split(",");
                if (nameList.Length > 1)
                {
                    for (int x = 0; x < nameList.Length; x++)
                    {
                        item.Add(nameList[x]);
                    }
                }
                else
                    item.Add(followMsg);
            }
            else
            {
                item.Add(followMsg);
            }

        }
    }

    //class WriteAndReadToFile
    //{

    //    private readonly string UserTextFile = ConfigurationManager.AppSettings["textFileName"];

    //    public void WriteUserToFile(Person person)
    //    {
    //        using (StreamWriter sw = new StreamWriter(UserTextFile, true))
    //        {
    //            sw.WriteLine(person.FirstName + "," + person.LastName + "," + person.Adress + ",");
    //        }
    //    }

    //    public void ReadFromFile(AdressBook ab)
    //    {
           
    //    }

    //    public void UpdateUserOnFile(List<Person> adressBookList)
    //    {
    //        // Remove old row
    //        using (StreamWriter sw = new StreamWriter(UserTextFile))
    //        {
    //            sw.Flush();
    //            foreach (var person in adressBookList)
    //            {
    //                sw.WriteLine(person.FirstName + "," + person.LastName + "," + person.Adress + ",");
    //            }
    //        }
    //    }
    //}
}
