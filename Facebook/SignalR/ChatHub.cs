using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Facebook.Models;
using Facebook.Models.OwnModels;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;

namespace Facebook.SignalR
{
    [HubName("chat")]

    public class ChatHub : Hub
    {
        ApplicationUser currentUser = new ApplicationUser();
        ApplicationDbContext DB = new ApplicationDbContext();
        protected override void Dispose(bool disposing)
        {
            DB.Dispose();
            base.Dispose(disposing);
        }
        List<UserConnection> ConIDs;

        public override Task OnConnected()
        {
            string id = Context.User.Identity.GetUserId();

            currentUser = DB.Users.FirstOrDefault(o => o.Id == id);
            DB.UserConnections.Add(new UserConnection()
            {
                ConnectionID = Context.ConnectionId,
                User = currentUser

            });
            DB.SaveChanges();
            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {

            UserConnection CurrentConnection = DB.UserConnections.Where(o => o.ConnectionID == Context.ConnectionId).FirstOrDefault();
            DB.SaveChanges();
            return base.OnDisconnected(stopCalled);
        
    }


    [HubMethodName("message")]
        public void NewMessageSent(string SenderID, string reseverID, string message)
        {
            ConIDs = DB.UserConnections.Where(o => ((o.User.Id == SenderID) || (o.User.Id == reseverID))).ToList();

            ApplicationUser Sender = DB.Users.Where(o => o.Id == SenderID).FirstOrDefault();
            ApplicationUser Resever = DB.Users.Where(o => o.Id == reseverID).FirstOrDefault();
            string className = "";
            for (int i = 0; i < ConIDs.Count(); i++)
            {
                if ((ConIDs[i].User.Id == currentUser.Id) && (currentUser.Id == Sender.Id))
                {
                    className = "sent";

                }
                else
                {
                    className = "replies";

                }
                string image=$"/Image/{Sender.image}";
                string Class = $"class={className}";
                Clients.Client(ConIDs[i].ConnectionID).newMessage(image, Sender.UserName, message, Class);
            }

            Messages m = new Messages()
            {
                Message = message,
                UserSender = Sender,
                UserResiver = Resever

            };
            DB.Messages.Add(m);
            DB.SaveChanges();
        }



    }
}