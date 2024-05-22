using ProjectFirst.Models;

namespace ProjectFirst.Infrastucture.Service
{
    public interface IOrder
    {
        int GetTotalAmount(int userId);

        int GetNetAmount(int userId);

        Order BuyNow(int userId);

        IEnumerable<Order> getAllOrders();

        void updateStatus(Guid orderId,string status);

        Order getOrderStatusByOrderId(Guid orderId);
    }
}
