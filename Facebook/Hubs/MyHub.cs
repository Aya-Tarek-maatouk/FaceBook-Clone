using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Facebook.Hubs
{
    public class MyHub : Hub
    {
        public static void Send()
        {

            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
            context.Clients.All.displayStatus();
        }
    }
}