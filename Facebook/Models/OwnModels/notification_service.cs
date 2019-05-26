using Facebook.Hubs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
namespace Facebook.Models.OwnModels
{
    public class notification_service
    {
        internal static SqlDependency dependency = null;
        public static string GetNotification()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            try
            {

                if (dependency == null)
                {
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                }
                var messages = context.Notifications.ToList();
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(messages);
                return json;

            }
            catch (Exception ex)
            { return null; }
        }


        private static void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (dependency != null)
            {
                dependency.OnChange -= dependency_OnChange;
                dependency = null;
            }
            if (e.Type == SqlNotificationType.Change)
            {
                MyHub.Send();
            }
        }

    }
}