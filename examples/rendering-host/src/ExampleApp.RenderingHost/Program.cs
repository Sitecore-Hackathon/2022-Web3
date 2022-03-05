using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using ExampleApp.RenderingHost.Configuration;
using ExampleApp.RenderingHost.Models;
using Sitecore.AspNet.RenderingEngine.Extensions;
using Sitecore.AspNet.RenderingEngine.Localization;
using Sitecore.LayoutService.Client.Extensions;
using Sitecore.LayoutService.Client.Newtonsoft.Extensions;
using Sitecore.LayoutService.Client.Request;
using System.Globalization;

var _defaultLanguage = "en";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting().AddLocalization().AddMvc().AddNewtonsoftJson(o => o.SerializerSettings.SetDefaults());

SitecoreOptions Configuration = builder.Configuration.GetSection(SitecoreOptions.Key).Get<SitecoreOptions>();

builder.Services.AddSitecoreLayoutService().WithDefaultRequestOptions(request =>
{
    request.SiteName(Configuration.DefaultSiteName)
           .ApiKey(Configuration.ApiKey);
}).AddHttpHandler("default", Configuration.LayoutServiceUri)
  .AsDefaultHandler();

builder.Services.AddSitecoreRenderingEngine(options =>
{
    options.AddModelBoundView<ContentBlockModel>("ContentBlock")
           .AddDefaultPartialView("_ComponentNotFound");
}).ForwardHeaders();

var app = builder.Build();

app.UseForwardedHeaders(ConfigureForwarding(app.Environment));

ForwardedHeadersOptions ConfigureForwarding(IWebHostEnvironment env)
{
    var options = new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    };

    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();

    return options;
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseRequestLocalization(options =>
{
    var supportedCultures = new List<CultureInfo> { new CultureInfo(_defaultLanguage) };

    options.DefaultRequestCulture = new RequestCulture(_defaultLanguage, _defaultLanguage);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.UseSitecoreRequestLocalization();
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("error", "error", new { controller = "Default", action = "Error" });
    endpoints.MapSitecoreLocalizedRoute("sitecore", "Index", "Default");
    endpoints.MapFallbackToController("Index", "Default");
});

app.Run();