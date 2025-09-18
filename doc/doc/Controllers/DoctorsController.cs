using Microsoft.AspNetCore.Mvc;
using doc.Models;
using doc.Services;

namespace doc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly DoctorService _doctorService;

        public DoctorsController(DoctorService doctorService) =>
            _doctorService = doctorService;

        // CREATE (POST)
        [HttpPost("register")]
        public async Task<IActionResult> RegisterDoctor([FromBody] DoctorModel doctor)
        {
            if (doctor == null)
                return BadRequest("Doctor data is required.");

            await _doctorService.AddDoctorAsync(doctor);
            return Ok("Doctor registered successfully.");
        }

        // READ ALL (GET)
        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            return Ok(doctors);
        }

        // READ ONE (GET by ID)
        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetDoctorById(string id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null)
                return NotFound($"Doctor with ID {id} not found.");

            return Ok(doctor);
        }

        // UPDATE (PUT)
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateDoctor(string id, [FromBody] DoctorModel doctorIn)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null)
                return NotFound($"Doctor with ID {id} not found.");

            doctorIn.Id = id; // keep same id
            var updated = await _doctorService.UpdateDoctorAsync(id, doctorIn);

            if (updated == null)
                return StatusCode(500, "Failed to update doctor.");

            return Ok("Doctor updated successfully.");
        }

        // DELETE
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteDoctor(string id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null)
                return NotFound($"Doctor with ID {id} not found.");

            var deleted = await _doctorService.DeleteDoctorAsync(id);
            if (!deleted)
                return StatusCode(500, "Failed to delete doctor.");

            return Ok("Doctor deleted successfully.");
        }

        [HttpGet("by-phone/{phone}")]
        public async Task<IActionResult> GetDoctorByPhone(string phone)
        {
            var doctor = await _doctorService.GetDoctorByPhoneAsync(phone);
            if (doctor == null)
            {
                return NotFound($"Doctor with phone number {phone} not found.");
            }

            return Ok(doctor);  // Return the doctor details as a JSON response
        }
    }
}
