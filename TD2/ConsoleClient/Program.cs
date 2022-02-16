//call API with GET request then display answer

var client = new HttpClient();
var response = await client.GetAsync("http://localhost:8080/MyMethods/incr?param1=2");
var result = await response.Content.ReadAsStringAsync();
Console.WriteLine(result);


