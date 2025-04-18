﻿using ps40165_User.Models;
using System.Net.Http.Json;

namespace ps40165_User.Services;

public class AccountService
{
    private readonly HttpClient _client;

    public AccountService(HttpClient client)
    {
        _client = client;
    }

    public async Task<Response<PaginatedList<Account>>> GetListAsync(int currentPage, int pageSize, string? searchText)
    {
        try
        {
            var response = await _client.GetFromJsonAsync<Response<PaginatedList<Account>>>($"Accounts?pageNumber={currentPage}&pageSize={pageSize}&searchTerm={searchText}");
            return response;
        }
        catch
        {

        }
        return new Response<PaginatedList<Account>>();
    }

    public async Task<Account> GetAccountById(int id)
    {
        try
        {
            var response = await _client.GetFromJsonAsync<Response<Account>>($"Accounts/{id}");

            if (response.IsSuccess)
            {
                if (response.Data is not null)
                    return response.Data;
            }
        }
        catch
        {

        }
        return new Account();
    }
}
