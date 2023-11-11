using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using Resto.Front.Api;
using Resto.Front.Api.Attributes;
using Resto.Front.Api.Attributes.JetBrains;
using Resto.Front.Api.Data.Device;
using Resto.Front.Api.Data.Device.Results;
using Resto.Front.Api.Data.Device.Settings;
using Resto.Front.Api.Devices;
using iiko_plugin_test_task.NotificationHandlers;

namespace iiko_plugin_test_task
{
    public sealed class MyPlugin : IFrontPlugin
    {
        private readonly Stack<IDisposable> subscriptions = new Stack<IDisposable>();

        public MyPlugin()
        {
            PluginContext.Log.Info("Initializing MyPlugin");
            subscriptions.Push(new TableChangedHandler());
            subscriptions.Push(new ReservesViewer());
            PluginContext.Log.Info("MyPlugin started");
        }

        public void Dispose()
        {
            while (subscriptions.Any())
            {
                var subscription = subscriptions.Pop();
                try
                {
                    subscription.Dispose();
                }
                catch (RemotingException)
                {
                }
            }

            PluginContext.Log.Info("MyPlugin stopped");
        }
    }
}
