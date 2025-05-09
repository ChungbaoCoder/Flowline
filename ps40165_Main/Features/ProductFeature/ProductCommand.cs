﻿using ps40165_Main.Dtos.GetDto;
using ps40165_Main.Dtos.PostDto;
using ps40165_Main.Dtos.PutDto;
using ps40165_Main.Models;
using ps40165_Main.Shared;
using ps40165_Main.Shared.Interfaces;

namespace ps40165_Main.Features.ProductFeature;

public class ProductCommand
{
    private readonly IProduct _svc;

    public ProductCommand(IProduct svc) => _svc = svc;

    public async Task<CentralResponse<List<Product>>> GetList()
    {
        var result = await _svc.GetListProducts();

        if (result.IsSuccess)
        {
            return new CentralResponse<List<Product>>
            {
                Messages = result.Messages,
                Data = result.Data
            };
        }
        else
        {
            return new CentralResponse<List<Product>>
            {
                Errors = result.Errors,
                Data = result.Data
            };
        }
    }

    public async Task<CentralResponse<Product>> GetById(int productId)
    {
        var result = await _svc.GetProductById(productId);

        if (result.IsSuccess)
        {
            return new CentralResponse<Product>
            {
                Messages = result.Messages,
                Data = result.Data
            };
        }
        else
        {
            return new CentralResponse<Product>
            {
                Errors = result.Errors,
                Data = result.Data
            };
        }
    }

    public async Task<CentralResponse<Product>> Create(CreateProductDto product)
    {
        var result = await _svc.CreateProduct(product);

        if (result.IsSuccess)
        {
            return new CentralResponse<Product>
            {
                Messages = result.Messages,
                Data = result.Data
            };
        }
        else
        {
            return new CentralResponse<Product>
            {
                Errors = result.Errors,
                Data = result.Data
            };
        }
    }

    public async Task<CentralResponse<Product>> Update(int productId, EditProductDto product)
    {
        var result = await _svc.UpdateProduct(productId, product);

        if (result.IsSuccess)
        {
            return new CentralResponse<Product>
            {
                Messages = result.Messages,
                Data = result.Data
            };
        }
        else
        {
            return new CentralResponse<Product>
            {
                Errors = result.Errors,
                Data = result.Data
            };
        }
    }

    public async Task<CentralResponse<Product>> Delete(int productId)
    {
        var result = await _svc.DeleteProduct(productId);

        if (result.IsSuccess)
        {
            return new CentralResponse<Product>
            {
                Messages = result.Messages,
                Data = result.Data
            };
        }
        else
        {
            return new CentralResponse<Product>
            {
                Errors = result.Errors,
                Data = result.Data
            };
        }
    }

    public async Task<CentralResponse<ProductDto>> GetDetail(int productId)
    {
        var result = await _svc.GetDetail(productId);

        if (result.IsSuccess)
        {
            return new CentralResponse<ProductDto>
            {
                Messages = result.Messages,
                Data = result.Data
            };
        }
        else
        {
            return new CentralResponse<ProductDto>
            {
                Errors = result.Errors,
                Data = result.Data
            };
        }
    }

    public async Task<CentralResponse<PaginatedList<Product>>> Paginate(int pageNumber, int pageSize, string? searchText)
    {
        var result = await _svc.GetPagination(pageNumber, pageSize, searchText);

        if (result.IsSuccess)
        {
            return new CentralResponse<PaginatedList<Product>>
            {
                Messages = result.Messages,
                Data = result.Data
            };
        }
        else
        {
            return new CentralResponse<PaginatedList<Product>>
            {
                Errors = result.Errors,
                Data = result.Data
            };
        }
    }
}
