using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands;

public class OrderCommand
{
    private readonly IApplicationDbContext _dbContext;
    private Order _order;

    public OrderCommand(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _order = new Order();
        _order.Items = new List<OrderItem>();
    }

    public List<OrderItem> GetItems() => _order.Items.ToList();

    public int GetTotalItemCount() => _order.Items.Sum(x => x.Amount);

    public void UpdateItem(OrderItem item, int amount)
    {
        if (amount < 1)
            RemoveItem(item);

        item.Amount = amount;
        item.CalculateTotalValue();
    }

    public void AddItem(Product product)
    {
        var item = new OrderItem(product);

        item.CalculateTotalValue();

        _order.AddItem(item);
    }

    public void SetObs(string? obs) => _order.SetObs(obs);

    public void RemoveItem(OrderItem item) => _order.Items.Remove(item);

    public void CalculateTotalValue() => _order.CalculateTotalValue();

    public decimal GetTotalValue() => _order.TotalValue;

    public Guid GetCode() => _order.Code;

    public void SetStatus(OrderStatus status) => _order.SetStatus(status);

    public OrderStatus GetStatus() => _order.Status;

    public async Task CreateOrUpdateAsync()
    {
        if (_order.Id > 0)
        {
            Order order = await _dbContext.Orders.Where(x => x.Id == _order.Id).FirstAsync();
            order.Status = _order.Status;
        }
        else
        {
            await _dbContext.Orders.AddAsync(_order);
        }

        await _dbContext.SaveChangesAsync(CancellationToken.None);
    }

    public async Task GetByCodeAsync(Guid code)
    {
        Order? order = await _dbContext.Orders.Where(x => x.Code == code)
            .FirstOrDefaultAsync();
        _order = order ?? throw new Exception();
    }

    public async Task<Order?> GetAsync(int id)
    {
        return await _dbContext.Orders.Where(x => x.Id == id)
            .FirstAsync();
    }

    public async Task<IEnumerable<Order>?> ListAllAsync()
    {
        return await _dbContext.Orders.ToListAsync();
    }
}