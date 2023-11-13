using ECommerce.API.Models;
using ECommerce.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IMessageProducer _messageProducer;
    public static readonly List<Product> _products = new();

    public ProductsController(
        ILogger<ProductsController> logger,
        IMessageProducer messageProducer)
    {
        _logger = logger;
        _messageProducer = messageProducer;
    }

    [HttpPost]
    public IActionResult CreateProduct(Product newProduct)
    {
        if (!ModelState.IsValid) return BadRequest();
        _products.Add(newProduct);
        _messageProducer.SendingMessage<Product>(newProduct);
        return Ok();
    }

    [HttpGet]
    public IActionResult GetAllProducts()
    {
        return Ok(_products);
    }
}
