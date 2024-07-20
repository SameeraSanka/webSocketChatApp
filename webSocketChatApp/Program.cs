using Fleck;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

var server = new WebSocketServer("ws://0.0.0.0:8181");
var wsConnection = new List<IWebSocketConnection>();

server.Start(ws =>
{
	ws.OnOpen = () =>
	{
		wsConnection.Add(ws);
	};
	ws.OnMessage = message =>
	{
		foreach (var webSocketConnection in wsConnection)
		{
			webSocketConnection.Send(message);
		}
	};
});

app.Run();
