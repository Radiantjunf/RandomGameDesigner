using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);






// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "cookie";
        options.DefaultAuthenticateScheme = "oidc";
    }
    )
    .AddCookie("cookie")
    .AddOpenIdConnect("oidc", options =>
     {
         options.Authority = "https://steamcommunity.com/openid/";
         options.ClientId = "cstrade";
         options.ClientSecret = "CB89CF1B8A1EB50BA8A4ABE1AC6920AD";
         options.ResponseType = "code";
         options.UsePkce = true;
         options.ResponseMode = "query";
         options.Scope.Add("weatherApi.read");
         options.SaveTokens = true; 
 


     });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
