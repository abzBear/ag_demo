using System;
using System.Collections.Generic;
using System.Text;

namespace ag_core
{
    interface IFileManagement
    {
        Dictionary<string, HashSet<string>> ReadFromFile(string fileName);
        bool CheckFileExist(string fileName);
        
    }
}
