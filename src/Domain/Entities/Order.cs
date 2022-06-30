using Domain.Common;
using Domain.Enum;

namespace Domain.Entities;

public class Order : BaseEntity
{
    public Order()
    {
        Code = Guid.NewGuid();
        Items = new List<OrderItem>();
        SetStatus(OrderStatus.Pending);
        Created = DateTime.Now;
        LastModified = DateTime.Now;
    }

    public Guid Code { get; set; }
    public decimal TotalValue { get; set; }
    public decimal DiscountValue { get; set; }
    public string? Obs { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderItem> Items { get; set; }

    public void AddItem(OrderItem item)
    {
        Items.Add(item);
        CalculateTotalValue();
    }
    
    public void CalculateTotalValue()
    {
        TotalValue = Items.Sum(x => x.TotalValue);
    }
    
    public void SetStatus(OrderStatus status)
    {
        Status = status;
    }
    
    public void SetObs(string? obs)
    {
        Obs = obs;
    }
}

public class OrderItem : BaseEntity
{
    public OrderItem()
    {
            
    }

    public OrderItem(Product product)
    {
        Product = product;
        ProductId = product.Id;
        Amount = 1;
        Created = DateTime.Now;
        LastModified = DateTime.Now;
    }
    
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int Amount { get; set; }
    public decimal TotalValue { get; set; }
    public virtual Order? Cart { get; set; }
    public virtual Product? Product { get; set; }

    public void CalculateTotalValue()
    {
        TotalValue = 0;
        
        if (Product == null)
            return;
        
        TotalValue = Product.UnitPrice * Amount;
    }
}