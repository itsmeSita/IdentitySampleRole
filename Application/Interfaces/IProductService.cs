using Application.Dtos;
using Application.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductService
    {
       Task<ServiceResponse<IEnumerable<ProductDto>>> GetAllProductsAsync();
        Task<ServiceResponse<ProductDto>> GetByIdAsync(int id);
        Task<ServiceResponse<ProductDto>> CreateProductAsync(CreateProductDto createProductDto);
        Task<ServiceResponse<bool>> UpdateProductAsync(int id, CreateProductDto updateProductDto);
        Task<ServiceResponse<bool>> DeleteProductAsync(int id);

        Task<ServiceResponse<ProductDto>>GetRandomProductByCategoryAsync(int categoryID);

    }
}
