using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using OrderWebApi.Models;

namespace OrderWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMongoCollection<Order> _orderCollection;

    public OrderController()
    {
        var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
        var dbName = Environment.GetEnvironmentVariable("DB_NAME");
        var connectionString = $"mongodb://{dbHost}:27017/{dbName}";

        var mongoUrl = MongoUrl.Create(connectionString);
        var mongoClient = new MongoClient(mongoUrl);
        var database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
        _orderCollection = database.GetCollection<Order>("order");
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        return await _orderCollection.Find(Builders<Order>.Filter.Empty).ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Order>> GetOrderById(int id)
    {
        var filterDefinition = Builders<Order>.Filter.Eq(query => query.Id, id);
        return await _orderCollection.Find(filterDefinition).SingleOrDefaultAsync();
    }

    [HttpPost]
    public async Task<ActionResult> CreateOrder(Order order)
    {
        await _orderCollection.InsertOneAsync(order);
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult<Order>> UpdateOrder(Order order)
    {
        var filterDefinition = Builders<Order>.Filter.Eq(query => query.Id, order.Id);
        await _orderCollection.ReplaceOneAsync(filterDefinition, order);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Order>> DeleteOrder(int id)
    {
        var filterDefinition = Builders<Order>.Filter.Eq(query => query.Id, id);
        await _orderCollection.DeleteOneAsync(filterDefinition);
        return Ok();
    }
}