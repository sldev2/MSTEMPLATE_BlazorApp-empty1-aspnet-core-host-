using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorPolicy", builder =>
    {
        builder.WithOrigins("https://promandex.com")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

// TOLEARN  when not commented out, got the Administrator liquidweb portal login!
// whether or not .AllowCredentials() was set
//app.Use(async (context, next) =>
//{
//    context.Response.Headers.Add("Referrer-Policy", "origin-when-cross-origin");
//    await next.Invoke();
//});

app.UseCors("BlazorPolicy"); // Moved after UseRouting

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
