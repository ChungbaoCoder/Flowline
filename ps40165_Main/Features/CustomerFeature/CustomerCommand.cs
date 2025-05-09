﻿using ps40165_Main.Dtos.PostDto;
using ps40165_Main.Dtos.PutDto;
using ps40165_Main.Models;
using ps40165_Main.Shared;
using ps40165_Main.Shared.Interfaces;

namespace ps40165_Main.Features.CustomerFeature;

public class CustomerCommand
{
    private readonly ICustomer _svc;

    public CustomerCommand(ICustomer svc) => _svc = svc;

    public async Task<CentralResponse<List<Customer>>> GetList()
    {
        var result = await _svc.GetListCustomers();

        if (result.IsSuccess)
        {
            return new CentralResponse<List<Customer>>
            {
                Messages = result.Messages,
                Data = result.Data
            };
        }
        else
        {
            return new CentralResponse<List<Customer>>
            {
                Errors = result.Errors,
                Data = result.Data
            };
        }
    }

    public async Task<CentralResponse<Customer>> GetById(int customerId)
    {
        var result = await _svc.GetCustomerById(customerId);

        if (result.IsSuccess)
        {
            return new CentralResponse<Customer>
            {
                Messages = result.Messages,
                Data = result.Data
            };
        }
        else
        {
            return new CentralResponse<Customer>
            {
                Errors = result.Errors,
                Data = result.Data
            };
        }
    }

    public async Task<CentralResponse<Customer>> Create(CreateCustomerDto customer)
    {
        var result = await _svc.CreateCustomer(customer);

        if (result.IsSuccess)
        {
            return new CentralResponse<Customer>
            {
                Messages = result.Messages,
                Data = result.Data
            };
        }
        else
        {
            return new CentralResponse<Customer>
            {
                Errors = result.Errors,
                Data = result.Data
            };
        }
    }

    public async Task<CentralResponse<Customer>> Update(int customerId, EditCustomerDto customer)
    {
        var result = await _svc.UpdateCustomer(customerId, customer);

        if (result.IsSuccess)
        {
            return new CentralResponse<Customer>
            {
                Messages = result.Messages,
                Data = result.Data
            };
        }
        else
        {
            return new CentralResponse<Customer>
            {
                Errors = result.Errors,
                Data = result.Data
            };
        }
    }

    public async Task<CentralResponse<Customer>> Delete(int customerId)
    {
        var result = await _svc.DeleteCustomer(customerId);

        if (result.IsSuccess)
        {
            return new CentralResponse<Customer>
            {
                Messages = result.Messages,
                Data = result.Data
            };
        }
        else
        {
            return new CentralResponse<Customer>
            {
                Errors = result.Errors,
                Data = result.Data
            };
        }
    }

    public async Task<CentralResponse<PaginatedList<Customer>>> Paginate(int pageNumber, int pageSize, string? searchText)
    {
        var result = await _svc.GetPagination(pageNumber, pageSize, searchText);

        if (result.IsSuccess)
        {
            return new CentralResponse<PaginatedList<Customer>>
            {
                Messages = result.Messages,
                Data = result.Data
            };
        }
        else
        {
            return new CentralResponse<PaginatedList<Customer>>
            {
                Errors = result.Errors,
                Data = result.Data
            };
        }
    }
}
