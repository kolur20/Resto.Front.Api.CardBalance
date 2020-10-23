namespace Resto.Front.Api.CardBalance
{
    internal class OrderItem : IOrderItem
    {
        string name;
        string kitchen;
        decimal cost;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public decimal Cost
        {
            get { return cost; }
            set { cost = value; }
        }
        public string Kitchen
        {
            get { return kitchen; }
            set { kitchen = value; }
        }

        public OrderItem(string name, decimal cost, string kitchen)
        {
            Name = name;
            Cost = cost;
            Kitchen = kitchen;
        }
    }
}