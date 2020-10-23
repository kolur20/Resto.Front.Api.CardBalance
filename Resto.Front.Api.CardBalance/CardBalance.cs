using Resto.Front.Api.Attributes;
using Resto.Front.Api.Attributes.JetBrains;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace Resto.Front.Api.CardBalance
{

    
    [UsedImplicitly]
    [PluginLicenseModuleId(21019307)]
    class CardBalance : IFrontPlugin
    {
        private readonly Stack<IDisposable> subscriptions = new Stack<IDisposable>();

        public CardBalance()
        {
            PluginContext.Log.Info("Initializing CardBalance");
            try
            {
                subscriptions.Push(new BillChequeExtender());

                //subscriptions.Push(ChequeTaskProcessor.Register());


            }
            catch (Exception ex)
            {
                PluginContext.Log.Error(ex.Message);
            }
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
                    // nothing to do with the lost connection
                }
            }

            PluginContext.Log.Info("CardBalance stopped");
        }
    }
}
