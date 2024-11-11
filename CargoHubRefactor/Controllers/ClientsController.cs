using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[Route("api/v1/ClientController")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Client>> GetClients()
    {
        var clients = _clientService.GetClients();
        if (clients == null || !clients.Any())
        {
            return NotFound("No clients found.");
        }

        return Ok(clients);
    }

    [HttpGet("{id}")]
    public ActionResult<Client> GetClient(int id)
    {
        var client = _clientService.GetClient(id);
        if (client == null)
        {
            return NotFound($"Client with ID {id} not found.");
        }

        return Ok(client);
    }

    [HttpPost]
    public ActionResult<Client> AddClient([FromBody] Client client)
    {
        if (string.IsNullOrEmpty(client.Name) ||
            string.IsNullOrEmpty(client.Address) ||
            string.IsNullOrEmpty(client.City) ||
            string.IsNullOrEmpty(client.ZipCode) ||
            string.IsNullOrEmpty(client.Province) ||
            string.IsNullOrEmpty(client.Country) ||
            string.IsNullOrEmpty(client.ContactName) ||
            string.IsNullOrEmpty(client.ContactPhone) ||
            string.IsNullOrEmpty(client.ContactEmail))
        {
            return BadRequest("Please provide values for all required fields.");
        }

        var newClient = _clientService.AddClient(client.Name, client.Address, client.City, client.ZipCode, client.Province,
                                                 client.Country, client.ContactName, client.ContactPhone, client.ContactEmail);

        return Ok(newClient);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateClient(int id, [FromBody] Client client)
    {
        if (string.IsNullOrEmpty(client.Name) ||
            string.IsNullOrEmpty(client.Address) ||
            string.IsNullOrEmpty(client.City) ||
            string.IsNullOrEmpty(client.ZipCode) ||
            string.IsNullOrEmpty(client.Province) ||
            string.IsNullOrEmpty(client.Country) ||
            string.IsNullOrEmpty(client.ContactName) ||
            string.IsNullOrEmpty(client.ContactPhone) ||
            string.IsNullOrEmpty(client.ContactEmail))
        {
            return BadRequest("Please provide values for all required fields.");
        }

        var updatedClient = _clientService.UpdateClient(id, client.Name, client.Address, client.City, client.ZipCode, client.Province,
                                                        client.Country, client.ContactName, client.ContactPhone, client.ContactEmail);
        if (updatedClient == null)
        {
            return NotFound($"Client with ID {id} not found.");
        }

        return Ok(updatedClient);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteClient(int id)
    {
        var isDeleted = _clientService.DeleteClient(id);
        if (!isDeleted)
        {
            return NotFound($"Client with ID {id} not found.");
        }

        return NoContent();
    }
}
