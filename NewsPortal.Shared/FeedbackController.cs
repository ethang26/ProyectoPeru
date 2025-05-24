[ApiController]
[Route("api/[controller]")]
public class FeedbackController : ControllerBase
{
    private readonly FeedbackContext _context;

    public FeedbackController(FeedbackContext context) => _context = context;

    [HttpPost]
    public async Task<IActionResult> PostFeedback([FromBody] Feedback feedback)
    {
        var existing = await _context.Feedbacks
            .AnyAsync(f => f.PostId == feedback.PostId);

        if (existing) return BadRequest("Ya existe feedback para este post");

        feedback.Fecha = DateTime.Now;
        _context.Feedbacks.Add(feedback);
        await _context.SaveChangesAsync();
        return Ok(feedback);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _context.Feedbacks.ToListAsync());
}
