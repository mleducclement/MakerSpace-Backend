using MakerSpace.Application.Interfaces.Services;
using MakerSpace.Application.Products.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace MakerSpace.Presentation.Controllers;

[Route("/products")]
[ApiController]
public class ProductController(IProductsService productsService) : ControllerBase {
   
   [HttpGet]
   public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetProducts() {
      var products = await productsService.GetAllAsync();
      return Ok(products);
   }

   [HttpGet("{id:guid}")]
   public async Task<ActionResult<ProductDto>> GetProduct(Guid id) {
      var product = await productsService.GetByIdAsync(id);

      if (product == null) {
         return NotFound();
      }

      return Ok(product);
   }

   [HttpPost]
   public async Task<ActionResult<ProductDto>> PostProduct(ProductMutateDto productDto) {
      var result = await productsService.CreateAsync(productDto);
      var createdProduct = result.Data;

      return CreatedAtAction(nameof(GetProduct), new { id = createdProduct!.Id }, createdProduct);
   }

   [HttpPost("bulk")]
   public async Task<ActionResult<IEnumerable<ProductDto>>> PostBulkProducts(IReadOnlyList<ProductMutateDto> productDtos) {
      var result = await productsService.CreateManyAsync(productDtos);
      var createdProducts = result.Data;

      return Ok(createdProducts);
   }
}