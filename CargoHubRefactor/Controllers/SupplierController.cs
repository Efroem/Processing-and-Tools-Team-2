using Microsoft.AspNetCore.Mvc;
using Services;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Controllers
{
    [Route("api/v1/suppliers")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        // Get all suppliers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetAllSuppliers()
        {
            var suppliers = await _supplierService.GetAllSuppliersAsync();
            return Ok(suppliers);
        }

        // Get supplier by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplierById(int id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);
            if (supplier == null)
                return NotFound();

            return Ok(supplier);
        }

        // Create new supplier
        [HttpPost]
        public async Task<ActionResult<Supplier>> CreateSupplier(Supplier supplier)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdSupplier = await _supplierService.CreateSupplierAsync(supplier);
            return CreatedAtAction(nameof(GetSupplierById), new { id = createdSupplier.SupplierId }, createdSupplier);
        }

        // Update existing supplier
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, Supplier supplier)
        {
            if (id != supplier.SupplierId)
                return BadRequest("Supplier ID mismatch.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _supplierService.UpdateSupplierAsync(id, supplier);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // Delete supplier
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var deleted = await _supplierService.DeleteSupplierAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
