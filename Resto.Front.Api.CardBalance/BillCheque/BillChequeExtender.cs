using System;
using System.Collections.Generic;

using Resto.Front.Api.Data.Cheques;
using Resto.Front.Api.Extensions;
using Resto.Front.Api.Attributes.JetBrains;
using Resto.Front.Api.Data.Orders;
using Resto.Front.Api.Data.Assortment;
using System.Linq;
using System.Xml.Linq;

namespace Resto.Front.Api.CardBalance
{
    /// <summary>
    /// Расширяет разметку пречека
    /// </summary>
    internal sealed class BillChequeExtender : IDisposable
    {
        [NotNull]
        private readonly IDisposable subscription;
        

        internal BillChequeExtender()
        {
            subscription = PluginContext.Notifications.SubscribeOnBillChequePrinting(AddBillChequeExtensions);
        }

        /// <summary>
        /// Добавить дополнительные поля к пречеку.
        /// </summary>        
        [NotNull]
        private BillCheque AddBillChequeExtensions(Guid orderId)
        {

            var costByKitchen = PluginContext.Operations.GetOrderById(orderId)
                    .Items
                    .Select(data => new OrderItem(
                        ((IOrderProductItem)data).Product.Name,
                        ((IOrderProductItem)data).Cost,
                        ((IOrderProductItem)data).Kitchen.Name
                        ))
                    .GroupBy(data => data.Kitchen)
                    .Select(data => new OrderItem(
                        string.Empty,
                        data.Select(item => item.Cost).Sum(),
                        data.Key
                        ));

            var customer = PluginContext.Operations.TryGetClientById(
                PluginContext.Operations.GetOrderById(orderId).CustomerIds.FirstOrDefault());

            return AddChequeExtensions(costByKitchen);
            
        }


        [NotNull]
        private static BillCheque AddChequeExtensions(IEnumerable<IOrderItem> order)
        {
            return new BillCheque
            {
                
                BeforeFooter = new XElement(Tags.WrapText,
                    order.Select(item =>
                        new XElement(Tags.Pair,
                            new XAttribute(Data.Cheques.Attributes.Left, item.Kitchen),
                            new XAttribute(Data.Cheques.Attributes.Right, string.Format("{0:C}", item.Cost))))                        )

            };
        }

        public void Dispose()
        {
            subscription.Dispose();
        }
    }
}
