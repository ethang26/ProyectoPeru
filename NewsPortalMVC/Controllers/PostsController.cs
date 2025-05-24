public class PostsController : Controller
{
    private readonly PostService _postService;
    private readonly HttpClient _localApi;

    public PostsController(PostService postService, IHttpClientFactory clientFactory)
    {
        _postService = postService;
        _localApi = clientFactory.CreateClient("LocalApi");
    }

    public async Task<IActionResult> Index() =>
        View(await _postService.GetAllPostsAsync());

    public async Task<IActionResult> Detail(int id) =>
        View(await _postService.GetPostDetail(id));

    [HttpPost]
    public async Task<IActionResult> SendFeedback(int postId, string sentimiento)
    {
        var result = await _localApi.PostAsJsonAsync("/api/feedback", new Feedback
        {
            PostId = postId,
            Sentimiento = sentimiento,
        });

        TempData["msg"] = result.IsSuccessStatusCode ? "Gracias por tu feedback" : "Ya enviaste tu opinión";
        return RedirectToAction("Detail", new { id = postId });
    }
}
