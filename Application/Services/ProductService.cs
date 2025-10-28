using Application.Dtos;
using Application.Interfaces;
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

        public ProductService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Product> AddAsync(ProductDto dto)
        {
            var entity = new Product { Name = dto.Name, price = dto.Price };
            await _uow.Products.AddAsync(entity);
            await _uow.SaveAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var prod = await _uow.Products.GetByIdAsync(id);
            if (prod == null) return;
            await _uow.Products.DeleteAsync(prod);
            await _uow.SaveAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
           
            return await _uow.Products.GetAllAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return  await _uow.Products.GetByIdAsync(id);
        }

        public async Task UpdateAsync(int id, ProductDto product)
        {
            var existingProduct =  _uow.Products.GetByIdAsync(id).Result;
            if (existingProduct == null) return;
            existingProduct.Name = product.Name;
            existingProduct.price = product.Price;
            await _uow.Products.UpdateAsync(existingProduct);
            await _uow.SaveAsync();
        }
    }
}
