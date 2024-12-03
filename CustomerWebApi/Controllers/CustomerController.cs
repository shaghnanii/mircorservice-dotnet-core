using CustomerWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerWebApi.Controllers;

[Route("api/customers")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly CustomerDbContext _customerDbContext;
    public CustomerController(CustomerDbContext customerDbContext)
    {
        _customerDbContext = customerDbContext;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<Customer>> GetCustomers()
    {
        return _customerDbContext.Customers;
    }
    
    [HttpGet("{customerId:int}")]
    public async Task<ActionResult<Customer>> GetCustomerById(int customerId)
    {
        var customer = await _customerDbContext.Customers.FindAsync(customerId);
        return customer;
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateCustomer(Customer customer)
    {
        await _customerDbContext.Customers.AddAsync(customer);
        await _customerDbContext.SaveChangesAsync();
        return Ok();
    }
    
    [HttpPut]
    public async Task<ActionResult> UpdateCustomer(Customer customer)
    {
        _customerDbContext.Customers.Update(customer);
        await _customerDbContext.SaveChangesAsync();
        return Ok();
    }
    
    [HttpDelete("{customerId:int}")]
    public async Task<ActionResult> DeleteCustomer(int customerId)
    {
        var customer = await _customerDbContext.Customers.FindAsync(customerId);
        _customerDbContext.Customers.Remove(customer);
        await _customerDbContext.SaveChangesAsync();
        return Ok();
    }
}