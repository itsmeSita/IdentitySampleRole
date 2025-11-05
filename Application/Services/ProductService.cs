using Application.Dtos;
using Application.Interfaces;
using Application.Response;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.User;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork uow , IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async  Task<ServiceResponse<ProductDto>> CreateProductAsync(CreateProductDto createProductDto)
        {
            var response = new ServiceResponse<ProductDto>();
            try 
            { 
                var product = _mapper.Map<Product>(createProductDto);
                await _uow.Products.AddAsync(product);
                var mappedProduct = _mapper.Map<ProductDto>(product);
                response.Data = mappedProduct;
                response.Success = true;
                response.Message = "Product created successfully.";
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error creating product: {ex.Message}";
                
            }
            return response;


        }

        public async  Task<ServiceResponse<bool>> DeleteProductAsync(int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var product = await _uow.Products.GetByIdAsync(id);
                if (product == null)
                {
                    response.Success = false;
                    response.Message = "Product not found.";
                    response.Data = false;
                    return response;
                }
                await _uow.Products.DeleteAsync(product);
                await _uow.SaveAsync();
                response.Success = true;
                response.Message = "Product deleted successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error deleting product: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<ProductDto>>> GetAllProductsAsync()
        {
            var response = new ServiceResponse<IEnumerable<ProductDto>>();
            try
            {
                var products = await _uow.Products.GetAllAsync(); 
                var mappedProducts = _mapper.Map<IEnumerable<ProductDto>>(products);
                response.Data = mappedProducts;
                response.Success = true;
                response.Message = "Products retrieved successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error retrieving products: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<ProductDto>> GetByIdAsync(int id)
        {
           var response = new ServiceResponse<ProductDto>();
            try
            {
                var product = await _uow.Products.GetByIdAsync(id);
                if (product == null)
                {
                    response.Success = false;
                    response.Message = "Product not found.";
                    return response;
                }
                var mappedProduct = _mapper.Map<ProductDto>(product);
                response.Data = mappedProduct;
                response.Success = true;
                response.Message = "Product retrieved successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error retrieving product: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<ProductDto>> GetRandomProductByCategoryAsync(int categoryID)
        {
            var response = new ServiceResponse<ProductDto>();
            try
            {
               
                var products = await _uow.Products.GetAsync(p => p.CategoryId == categoryID);
                var productList = products.ToList();
                if (!productList.Any())
                {
                    response.Success = false;
                    response.Message = "No products found in this category.";
                    return response;
                }
                var random = new Random();
                var randomProduct = productList[random.Next(productList.Count)];
                response.Data = _mapper.Map<ProductDto>(randomProduct);
                response.Success = true;
                response.Message = "Random product retrieved successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error retrieving random product: {ex.Message}";
            }
            return response;
        }
        public async Task<ServiceResponse<bool>> UpdateProductAsync(int id, CreateProductDto updateProductDto)
        {
          var response = new ServiceResponse<bool>();
            try
            {
                var product = await _uow.Products.GetByIdAsync(id);
                if (product == null)
                {
                    response.Success = false;
                    response.Message = "Product not found.";
                    response.Data = false;
                    return response;
                }
                _mapper.Map(updateProductDto, product);
                await _uow.Products.UpdateAsync(product);
                response.Success = true;
                response.Message = "Product updated successfully.";
                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error updating product: {ex.Message}";
                response.Data = false;
            }
            return response;
        }
    }
}
