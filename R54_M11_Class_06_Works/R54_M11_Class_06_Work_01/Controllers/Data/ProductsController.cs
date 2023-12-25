using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R54_M11_Class_06_Work_02.Models;

namespace R54_M11_Class_06_Work_01.Controllers.Data
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Staff,Admin")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductDbContext db;
        public ProductsController(ProductDbContext db)
        {
            this.db = db;
        }
        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await db.Products.ToListAsync();
            return products;
        }
    }
}
