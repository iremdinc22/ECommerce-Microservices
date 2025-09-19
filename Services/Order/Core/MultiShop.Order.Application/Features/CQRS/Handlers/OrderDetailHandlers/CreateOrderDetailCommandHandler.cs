using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Features.CQRS.Results.OrderDetailResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers;

public class CreateOrderDetailCommandHandler
{
    private readonly IRepository<OrderDetail> _repository;

    public CreateOrderDetailCommandHandler(IRepository<OrderDetail> repository)
    {
        _repository = repository;
    }

    public async Task Handle(CreateOrderDetailCommand createOrderDetailCommand)
    { 
        await _repository.CreateAsync(new OrderDetail
        {
            ProductId = createOrderDetailCommand.ProductId,
            ProductName = createOrderDetailCommand.ProductName,
            ProductPrice = createOrderDetailCommand.ProductPrice,
            ProductAmount = createOrderDetailCommand.ProductAmount,
            ProductTotalPrice = createOrderDetailCommand.ProductTotalPrice,
            OrderingId =  createOrderDetailCommand.OrderingId
            
        });
        
    }
}