using Microsoft.AspNetCore.Mvc;
using ShopifyOpsSync.Infrastructure.Services;

namespace ShopifyOpsSync.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShopifyTestController(ShopifyGraphQLService shopifyService) : ControllerBase
    {

        [HttpGet("products")]
        public async Task<IActionResult> GetProducts()
        {
            var query = @"{ products(first: 5) { edges { node { id title handle status } } } }";
            var result = await shopifyService.QueryAsync(query);
            return Ok(result);
        }
    }
}