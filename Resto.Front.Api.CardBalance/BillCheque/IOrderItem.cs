namespace Resto.Front.Api.CardBalance
{
    interface IOrderItem
    {
        string Name { get; set; }
        decimal Cost { get; set; }
        string Kitchen { get; set; }
    }
}