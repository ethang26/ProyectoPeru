public class PostService
{
    private readonly HttpClient _http;

    public PostService(HttpClient http) => _http = http;

    public async Task<List<Post>> GetAllPostsAsync() =>
        await _http.GetFromJsonAsync<List<Post>>("https://jsonplaceholder.typicode.com/posts");

    public async Task<PostDetailDTO> GetPostDetail(int id)
    {
        var post = await _http.GetFromJsonAsync<Post>($"https://jsonplaceholder.typicode.com/posts/{id}");
        var author = await _http.GetFromJsonAsync<User>($"https://jsonplaceholder.typicode.com/users/{post.UserId}");
        var comments = await _http.GetFromJsonAsync<List<Comment>>($"https://jsonplaceholder.typicode.com/comments?postId={id}");

        return new PostDetailDTO { Post = post, Author = author, Comments = comments };
    }
}
