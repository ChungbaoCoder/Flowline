using ps40165_Main.Dtos.PostDto;
using ps40165_Main.Dtos.PutDto;
using ps40165_Main.Models;
using ps40165_Main.Shared.ModelResult;

namespace ps40165_Main.Shared.Interfaces;

public interface ICustomer
{
    Task<Result<List<Customer>>> GetListCustomers();

    Task<Result<Customer>> GetCustomerById(int customerId);

    Task<Result<Customer>> GetCustomerByEmail(string email);

    Task<Result<Customer>> CreateCustomer(CreateCustomerDto customer);

    Task<Result<Customer>> UpdateCustomer(int customerId, EditCustomerDto customer);

    Task<Result<Customer>> DeleteCustomer(int customerId);

    Task<Result<PaginatedList<Customer>>> GetPagination(int pageNumber, int pageSize, string? searchText);

    Task<Result<Customer>> CheckEmailExist(string email);
}
