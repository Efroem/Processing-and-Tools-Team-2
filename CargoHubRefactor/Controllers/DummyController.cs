
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

[Route("dummy")]
public class DummyController : Controller {

    [HttpPost()]
    public async Task<IActionResult> PostEmployee() {
        return NotFound("unimplemented");
    }

    [HttpGet()]
    public async Task<IActionResult> GetEmployee([FromQuery] Guid id = new Guid()) {
        return Ok(new Dummy { Name = "Xander", Age = 20 });
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteEmployee([FromQuery] Guid id) {
        return NotFound("unimplemented");
        
    }
}