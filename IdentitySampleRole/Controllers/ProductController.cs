using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentitySampleRole.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _ProductService;
        public ProductController(IProductService productService)
        {
            _ProductService = productService;
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _ProductService.GetAllProductsAsync();
            return Ok(products);
        }


        [HttpGet]
        [Route("GetProductById/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _ProductService.GetByIdAsync(id);
            if (product.Data == null)
            {
                return NotFound(product.Message);
            }
            return Ok(product);
        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _ProductService.CreateProductAsync(createProductDto);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result.Message);
            }
            return BadRequest("Invalid model state.");
        }


        [HttpPut]
        [Route("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] CreateProductDto updateProductDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _ProductService.UpdateProductAsync(id, updateProductDto);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result.Message);
            }
            return BadRequest("Invalid model state.");
        }

        [HttpDelete]
        [Route("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (ModelState.IsValid)
            {
                var result = await _ProductService.DeleteProductAsync(id);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result.Message);
            }
            return BadRequest();
        }
    }
 }
