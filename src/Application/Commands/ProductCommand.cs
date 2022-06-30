using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands;

public class ProductCommand
{
    private readonly IApplicationDbContext _dbContext;
    
    public ProductCommand(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateOrUpdateAsync(Product product)
    {
        if (product.Id > 0)
        {
            Product entity = await _dbContext.Products.Where(x => x.Id == product.Id).FirstAsync();

            entity.Name = product.Name;
            entity.Description = product.Description;
            entity.UnitPrice = product.UnitPrice;
            entity.Type = product.Type;
            entity.LastModified = DateTime.Now;
        }
        else
        {
            _dbContext.Products.Add(product);    
        }
        
        await _dbContext.SaveChangesAsync(CancellationToken.None);
    }
    
    public async Task<Product?> GetAsync(int id)
    {
        return await _dbContext.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> ListAllAsync()
    {
        return await _dbContext.Products.ToListAsync();
    }
    
    public async Task<IEnumerable<Product>> ListAllAsync(ProductType type)
    {
        return await _dbContext.Products.Where(x => x.Type == type)
            .ToListAsync();
    }
}