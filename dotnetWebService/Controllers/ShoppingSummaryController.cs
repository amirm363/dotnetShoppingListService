using dotnetWebService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("[controller]")]
public class ShoppingSummaryController : ControllerBase
{
    private readonly MyDbContext _context;
    private readonly ILogger<ShoppingSummaryController> _logger;

    public ShoppingSummaryController(MyDbContext context,ILogger<ShoppingSummaryController> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    }

    [HttpPost]
    public async Task<IActionResult> PostShoppingSummary([FromBody] ShoppingSummary shoppingSummary)
    {
        // Check if the received data is valid
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            // Add the shoppingSummary to the context and save changes to the database
            _context.shoppingSummary.Add(shoppingSummary);
            await _context.SaveChangesAsync();

            // Return a successful response with the saved shoppingSummary
            return Ok(shoppingSummary);
        }
        catch (Exception ex)
        {
            // Log the error and return an error response
            _logger.LogError(ex, "An error occurred while saving changes to the database: {InnerException}", ex.InnerException);
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}
