using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public IActionResult GetClients()
    {
        var clients = _clientService.GetClients();
        return Ok(clients);
    }

    [HttpGet("{id}")]
    public IActionResult GetClient(int id)
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

        var existingClient = _clientService.GetClient(id);
        if (existingClient == null)
        {
            return NotFound($"Client with ID {id} not found.");
        }

        var updatedClient = _clientService.UpdateClient(id, client.Name, client.Address, client.City, client.ZipCode, client.Province,
                                                        client.Country, client.ContactName, client.ContactPhone, client.ContactEmail);

        return Ok(updatedClient);
    }


    [HttpDelete("{id}")]
    public IActionResult DeleteClient(int id)
    {
        var removed = _clientService.DeleteClient(id);
        if (!removed)
        {
            return NotFound($"Client with ID {id} could not be found.");
        }
        return NoContent();
    }
}
