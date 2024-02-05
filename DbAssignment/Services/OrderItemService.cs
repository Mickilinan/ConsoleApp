using DbAssignment.Entities;
using DbAssignment.Repositories;

namespace DbAssignment.Services;

public class OrderItemService(OrderItemRepository orderItemRepository, OrderService orderService, ProductService productService)
{

    private readonly OrderItemRepository _orderItemRepository = orderItemRepository;
    private readonly OrderService  _orderService = orderService;
    private readonly ProductService _productService = productService;

    

    public OrderItemEntity CreateOrderItem(int quantity, int orderId, int productId)
    {
       

        
        var orderItemEntity = new OrderItemEntity
        {
            Quantity = quantity,
            OrderId = orderId,
            ProductId = productId
        };

        orderItemEntity = _orderItemRepository.Create(orderItemEntity);

        return orderItemEntity;
      
    }

    public OrderItemEntity GetOrderItemById(int id)
    {
        var orderItemEntity = _orderItemRepository.Get(x => x.Id == id);
        return orderItemEntity;
    }

    public IEnumerable <OrderItemEntity> GetOrderItemsByOrderId(int orderId)
    {
        var orderItemEntities = _orderItemRepository.GetAll(x => x.OrderId == orderId);
        return orderItemEntities;
    }

    public IEnumerable<OrderItemEntity> GetAllOrderItems()
    {
        var orderitems = _orderItemRepository.GetAll();
        return orderitems;
    }

    public OrderItemEntity UpdateOrderItem(OrderItemEntity orderItemEntity)
    {
        var updatedOrderItem = _orderItemRepository.Update(x => x.Id == orderItemEntity.Id, orderItemEntity);
        return updatedOrderItem;
    }


    public void DeleteOrderItem(int Id)
    {
        _orderItemRepository.Delete(x => x.Id == Id);

    }
}



