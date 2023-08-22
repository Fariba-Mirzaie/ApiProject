// See https://aka.ms/new-console-template for more information


using MyWebAPI.Client;
using System.Collections.Generic;
using System.Net.Http.Json;

HttpClient client = new HttpClient();
client.BaseAddress =new Uri("https://localhost:7205");
client.DefaultRequestHeaders.Clear();
client.DefaultRequestHeaders.Add("Accept", "application/json");

HttpResponseMessage responseMessage = await client.GetAsync("api/IssueAPI");
responseMessage.EnsureSuccessStatusCode();
if (responseMessage.IsSuccessStatusCode)
{
    var issues = await responseMessage.Content.ReadFromJsonAsync<IEnumerable<IssueDTO>>();
    foreach (var item in issues)
    {
        Console.WriteLine(item.Title);
    }
}
else
    Console.WriteLine("No Result");



