using TemperatuurDeken.Models;

static async Task Main(string[] args)
{
    var httpClient = new HttpClient();
    var apiService = new ApiService(httpClient);

    var posts = await apiService.GetPostsAsync();
    foreach (var post in posts)
    {
        Console.WriteLine($"{post.Datum}: {post.Temperatuur}");
    }
}
