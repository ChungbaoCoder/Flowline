using Microsoft.EntityFrameworkCore;
using ps40165_Main.Commands;
using ps40165_Main.Database;
using ps40165_Main.Database.DbResponse;
using ps40165_Main.Database.DbResponse.ErrorDoc;
using ps40165_Main.Dtos;
using ps40165_Main.Mapper.ModelToDto;
using ps40165_Main.Models;

namespace ps40165_Main.Services;

public class OrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IDbResponse<PaginatedList<OrderDto>>> GetListOrder(QueryPageCommand request)
    {
        int pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;
        int pageSize = request.PageSize < 1 ? 10 : request.PageSize;

        var orders = await _context.Orders
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
            .Select(o => new Order
            {
                Id = o.Id,
                Customer = o.Customer,
                OrderDate = o.OrderDate,
                Status = o.Status,
                Completed = o.Completed,
                Total = o.Total,
                OrderItems = o.OrderItems,
                CreatedOnUtc = o.CreatedOnUtc,
                UpdatedOnUtc = o.UpdatedOnUtc
            })
            .ToListAsync();

        if (orders.Count < 1)
        {
            return DbResponse<PaginatedList<OrderDto>>.Failure(new ProductError().NotFound());
        }

        int totalCount = await _context.Products.CountAsync();
        int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        List<OrderDto> data = new List<OrderDto>();

        foreach (Order order in orders)
            data.Add(new OrderMapper().Map(order));

        PaginatedList<OrderDto> meta = new PaginatedList<OrderDto>(data, pageNumber, pageSize, totalCount);

        return DbResponse<PaginatedList<OrderDto>>.GiveBack(meta);
    }

    public async Task<IDbResponse<OrderDto>> MakeOrder(MakeOrderCommand request)
    {
        List<int> productIds = request.Items.Select(c => c.ProductId).Distinct().ToList();

        List<Product> products = await _context.Products
            .Where(p => productIds.Contains(p.Id))
            .ToListAsync();

        OrderError error = new OrderError();
        bool miss = false;

        foreach (Product product in products)
        {
            foreach (OrderedItemCommand orderedItem in request.Items)
            {
                if (product.Id == orderedItem.ProductId)
                {
                    if (product.StockLevel > orderedItem.Quantity)
                    {
                        product.StockLevel -= orderedItem.Quantity;
                        orderedItem.SKU = product.SKU;
                        orderedItem.ImagePath = product.ProductImages.FirstOrDefault(pi => pi.MainImage == true)?.ImagePath;
                        orderedItem.Price = product.Price;
                    }
                    else
                    {
                        miss = true;
                        error.LowStock(product.StockLevel, orderedItem.Quantity);
                    }
                }
            }
        }

        List<OrderItem> orderItems = request.Items
            .GroupBy(item => item.ProductId)
            .Select(group => new OrderItem
            {
                ProductId = group.Key,
                Quantity = group.Sum(item => item.Quantity),
                SKU = group.First().SKU,
                ImagePath = group.First().ImagePath,
                Price = group.First().Price,
            })
            .ToList();

        decimal total = orderItems.Sum(oi => oi.Total);

        Order order = new Order
        {
            AccountId = request.AccountId,
            OrderDate = request.OrderDate,
            Status = request.Status,
            Total = total,
            OrderItems = orderItems
        };

        if (!miss)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }
        else
            return DbResponse<OrderDto>.Failure(error);

        OrderDto data = new OrderMapper().Map(order);

        return DbResponse<OrderDto>.GiveBack(data);
    }
}
