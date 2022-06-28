using System;
using System.Collections.Generic;
using System.Text;

namespace ag_core
{
    interface IFeed
    {
        void FindUserFeed(string username);

        void GetFeedList();
    }
}
