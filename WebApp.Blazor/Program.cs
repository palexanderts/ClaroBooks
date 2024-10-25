using Data;
using WebApp.Blazor;
using WebApp.Blazor.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<BookServiceOnline>();
builder.Services.AddHttpClient(string.Empty, e =>
{
    e.BaseAddress = new Uri(builder.Configuration.GetConnectionString("backend"));
});

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
