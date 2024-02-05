using DbAssignment.Entities;
using DbAssignment.Repositories;

namespace DbAssignment.Services;

public class OrderService(OrderRepository orderRepository, UserService userService)
{

    private readonly OrderRepository _orderRepository = orderRepository;
    private readonly UserService _userService = userService;

    public OrderEntity CreateOrder(string status, DateTime createdAt, DateTime updatedAt, string firstName, string lastName, string email )
    {
        var userEntity = _userService.CreateUser(firstName,lastName, email);
       
        var orderEntity = new OrderEntity
        {
            Status = status,
            CreatedAt = createdAt,
            UpdatedAt = updatedAt,
            UserId = userEntity.Id
        };

        orderEntity = _orderRepository.Create(orderEntity);

        return orderEntity;
     

    }

    public OrderEntity GetOrderById(int id)
    {
        var orderEntity = _orderRepository.Get(x => x.Id == id);
        return orderEntity;
    }

    public IEnumerable<OrderEntity> GetAllOrders()
    {
        var orders = _orderRepository.GetAll();
        return orders;
    }

    public OrderEntity UpdateOrder(OrderEntity orderEntity)
    {
        var updatedOrder = _orderRepository.Update(x => x.Id == orderEntity.Id, orderEntity);
        return updatedOrder;
    }


    public void DeleteOrder(int Id)
    {
        _orderRepository.Delete(x => x.Id == Id);

    }
}
