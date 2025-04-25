using Microsoft.EntityFrameworkCore;
using ps40165_Main.Database;
using ps40165_Main.Models;
using ps40165_Main.Shared.Interfaces;
using ps40165_Main.Shared.ModelResult;

namespace ps40165_Main.Features.CustomerFeature;

public class CustomerService : ICustomer
{
    private readonly AppDbContext _context;

    public CustomerService(AppDbContext context) => _context = context;

    public async Task<Result<List<Customer>>> GetListCustomers()
    {
        var customers = await _context.Customers
            .AsNoTracking()
            .ToListAsync();

        if (customers.Count < 1)
            return Result<List<Customer>>.Fail("Không có khách hàng nào trong danh sách");

        return Result<List<Customer>>.Ok(customers, "Lấy danh sách khách hàng thành công");
    }

    public async Task<Result<Customer>> GetCustomerById(int customerId)
    {
        var cus = await _context.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == customerId);

        if (cus is null)
            return Result<Customer>.Fail($"Không tìm thấy khách hàng có mã id {customerId}");

        return Result<Customer>.Ok(cus, $"Tìm thấy khách hàng có mã id {customerId}");
    }

    public async Task<Result<Customer>> CreateCustomer(Customer customer)
    {
        var cus = new Customer();
        Result<Customer> result = cus.UpdateName(customer.Name)
            .Bind(r => cus.UpdateEmail(customer.Email));

        if (result.IsSuccess)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            result = Result<Customer>.Ok("Tạo khách hàng mới thành công");
        }

        return result;
    }

    public async Task<Result<Customer>> UpdateCustomer(int customerId, Customer customer)
    {
        var cus = await _context.Customers.FindAsync(customerId);

        if (cus is null)
            return Result<Customer>.Fail($"Không tìm thấy khách hàng có mã id {customerId}");

        Result<Customer> result = cus.UpdateName(customer.Name)
            .Bind(r => cus.UpdateEmail(customer.Email));

        if (result.IsSuccess)
        {
            await _context.SaveChangesAsync();
            result = Result<Customer>.Ok($"Cập nhật khách hàng có mã id {customerId} thành công");
        }

        return result;
    }

    public async Task<Result<Customer>> DeleteCustomer(int customerId)
    {
        var cus = await _context.Customers.FindAsync(customerId);

        if (cus is null)
            return Result<Customer>.Fail($"Không tìm thấy khách hàng có mã id {customerId}");

        _context.Customers.Remove(cus);
        await _context.SaveChangesAsync();
        return Result<Customer>.Ok($"Xóa khách hàng có mã id {customerId} thành công");
    }
}
