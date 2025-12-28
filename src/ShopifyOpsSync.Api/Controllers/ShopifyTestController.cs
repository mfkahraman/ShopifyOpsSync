using Microsoft.AspNetCore.Mvc;
using ShopifyOpsSync.Domain.Models;
using ShopifyOpsSync.Infrastructure.Services;

namespace ShopifyOpsSync.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShopifyTestController(ShopifyGraphQLService shopifyService) : ControllerBase
    {
        [HttpGet("products")]
        public async Task<ActionResult<List<ShopifyProductNode>>> GetProducts()
        {
            var query = @"{ products(first: 5) { edges { node { id title handle status } } } }";
            var products = await shopifyService.GetProductsAsync(query);
            return Ok(products);
        }
    }
}