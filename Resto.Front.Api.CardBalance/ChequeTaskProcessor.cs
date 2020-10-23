using Resto.Front.Api.Attributes.JetBrains;
using Resto.Front.Api.Data.Cheques;
using Resto.Front.Api.Data.Device;
using Resto.Front.Api.Data.Device.Results;
using Resto.Front.Api.Data.Device.Tasks;
using Resto.Front.Api.Data.Security;
using Resto.Front.Api.Devices.ChequeTaskProcessor;
using Resto.Front.Api.Exceptions;
using Resto.Front.Api.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Resto.Front.Api.CardBalance
{

    internal sealed class ChequeTaskProcessor : IChequeTaskProcessor
    {




        public void AfterDoCheckAction([NotNull] ChequeTask chequeTask, [NotNull] PostResult result, [NotNull] ICashRegisterInfo device, IViewManager viewManager)
        {
            
        }

        public void AfterPayIn([NotNull] ICashRegisterInfo device, decimal sum, [NotNull] PostResult result, IViewManager viewManager)
        {
            
        }

        public void AfterPayOut([NotNull] ICashRegisterInfo device, decimal sum, [NotNull] PostResult result, IViewManager viewManager)
        {
            
        }

        public void AfterXReport([NotNull] ICashRegisterInfo device, [NotNull] PostResult result, IViewManager viewManager)
        {
            
        }

        public void AfterZReport([NotNull] ICashRegisterInfo device, [NotNull] PostResult result, IUser authUser, IViewManager viewManager)
        {
            
        }

        public BeforeDoCheckActionResult BeforeDoCheckAction([NotNull] ChequeTask chequeTask, [NotNull] ICashRegisterInfo device, [NotNull] CashRegisterChequeExtensions chequeExtensions, IViewManager viewManager)
        {
            var beforeCheque = new List<Data.Print.Document>();
            var documentBefore = new Data.Print.Document();
            documentBefore.Markup.Add(new XElement(Tags.LargeFont, "Welcome"));
            documentBefore.Markup.Add(new XElement(Tags.SmallFont, "tel. 555-123456"));
            beforeCheque.Add(documentBefore);

            var afterCheque = new List<Data.Print.Document>();
            var documentAfter = new Data.Print.Document();
            documentBefore.Markup.Add(new XElement(Tags.SmallFont, "Thank you for shopping"));
            documentBefore.Markup.Add(new XElement(Tags.QRCode, "iiko.ru"));
            beforeCheque.Add(documentAfter);

            return new BeforeDoCheckActionResult
            {
                BeforeCheque = beforeCheque,
                AfterCheque = afterCheque,
                CashierName = "CashierName"
            };
        }

        public void BeforeXReport([NotNull] ICashRegisterInfo device, IUser authUser, IViewManager viewManager)
        {
           
        }

        public void BeforeZReport([NotNull] ICashRegisterInfo device, decimal cashRest, IUser authUser, IViewManager viewManager)
        {
           
        }

        public static IDisposable Register()
        {
            IDisposable subscription = null;
            try
            {
                subscription = PluginContext.Operations.RegisterChequeTaskProcessor(new ChequeTaskProcessor());
                PluginContext.Log.Warn("ChequeTaskProcessor was registered.");
            }
            catch (LicenseRestrictionException ex)
            {
                PluginContext.Log.Warn(ex.Message);
            }

            return subscription;
        }
    }
}
