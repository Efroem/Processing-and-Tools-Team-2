
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

[Route("dummy")]
public class DummyController : Controller {

    [HttpPost()]
    public async Task<IActionResult> PostDummy() {
        return NotFound("unimplemented");
    }

    [HttpGet()]
    public async Task<IActionResult> GetDummy() {
        return Ok(new Dummy { Name = "Xander", Age = 20 });
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteDummy() {
        return NotFound("unimplemented");
    }
}