using Microsoft.EntityFrameworkCore;
using NewsPortal.Shared;

public class FeedbackContext : DbContext
{
    public FeedbackContext(DbContextOptions<FeedbackContext> options) : base(options) { }
    public DbSet<Feedback> Feedbacks { get; set; }
}
