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

        var order = _orderService.GetOrderById(orderId);
        var product = _productService.GetProductById(productId);

        if (order == null || product == null)
        {
            return null;
        }

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
        var existingOrderItem = _orderItemRepository.Update(x => x.Id == orderItemEntity.Id, orderItemEntity);
        if (existingOrderItem == null)
        {
            return null;
        }

        existingOrderItem.Quantity = orderItemEntity.Quantity;
        existingOrderItem.OrderId = orderItemEntity.OrderId;
        existingOrderItem.ProductId = orderItemEntity.ProductId;

        _orderItemRepository.Update(x => x.Id == existingOrderItem.Id, existingOrderItem);
        return existingOrderItem;
    }

    public bool DeleteOrderItem(int Id)
    {
        var existingOrderItem = _orderItemRepository.Get(x => x.Id == Id);
        if (existingOrderItem == null)
        {
            return false;
        }

        _orderItemRepository.Delete(x => x.Id == Id);
        return true;

    }
}



