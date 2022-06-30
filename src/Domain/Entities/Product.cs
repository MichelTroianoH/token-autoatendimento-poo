using Domain.Common;
using Domain.Enum;

namespace Domain.Entities;

public class Product : BaseEntity
{
    public Product()
    {
        
    }
    
    public Product(string name, decimal unitPrice, ProductType productType)
    {
        Name = name;
        UnitPrice = unitPrice;
        Type = productType;
        Created = DateTime.Now;
        LastModified = DateTime.Now;
    }
    
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal UnitPrice { get; set; }
    public ProductType Type { get; set; }

    public void ValidateFields()
    {
        if(string.IsNullOrEmpty(Name))
            throw new Exception($"{nameof(Name)} is required.");
        
        if(UnitPrice < 0)
            throw new Exception($"{nameof(UnitPrice)} must be greater than 0.");
        
        if(Type != ProductType.Burguer && 
           Type != ProductType.Combo && 
           Type != ProductType.Dessert && 
           Type != ProductType.Drink)
            throw new Exception($"{nameof(Type)} product is invalid.");
    }

    public void SetDescription(string? description)
    {
        Description = description;
    }
}