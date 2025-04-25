using ps40165_Main.Models;
using ps40165_Main.Shared.ModelResult;

namespace ps40165_Main.Shared.Interfaces;

public interface ICustomer
{
    Task<Result<List<Customer>>> GetListCustomers();

    Task<Result<Customer>> GetCustomerById(int customerId);

    Task<Result<Customer>> CreateCustomer(Customer customer);

    Task<Result<Customer>> UpdateCustomer(int customerId, Customer customer);

    Task<Result<Customer>> DeleteCustomer(int customerId);
}
