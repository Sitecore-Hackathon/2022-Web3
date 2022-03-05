using Sitecore.AspNet.RenderingEngine.Binding.Attributes;
using Sitecore.LayoutService.Client.Response.Model.Fields;

namespace ExampleApp.RenderingHost.Models
{
    public class PageModel
    {
        [SitecoreRouteField]
        public TextField Title { get; set; }

        [SitecoreRouteField]
        public ImageField Banner { get; set; }

        public string CopyrightYear => DateTime.Now.ToString("yyyy");
    }
}
