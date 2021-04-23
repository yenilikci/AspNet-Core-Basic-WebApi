using ApiBasic.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBasic.Controllers
{
    // Ok 200, Created 201, NoContent 204
    // HttpGet, HttpPost, HttpPut, HttpDelete
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly Context _context;
        public ProductsController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_context.Products.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            return Ok(_context.Products.FirstOrDefault(I => I.Id == id));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Product product)
        {
            var updatedProduct = _context.Products.FirstOrDefault(I => I.Id == id);
            updatedProduct.Name = product.Name;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var deleteProduct = _context.Products.FirstOrDefault(I => I.Id == id);
            _context.Remove(deleteProduct);
            return NoContent();
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();   
            return Created("",product);
        }

    }
}
