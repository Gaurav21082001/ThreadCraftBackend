using ProjectFirst.Models;

namespace ProjectFirst.Infrastucture.Service
{
    public interface IOrder_Item
    {
        Task<IEnumerable<Order_Items>> GetOrderedItems(Guid orderId);

        IEnumerable<Order_Items> GetAllOrders();
        void AddOrderdItems(int userId, Guid orderId);
        IEnumerable<Order_Items> GetMyOrders(int userId);
    }
}
