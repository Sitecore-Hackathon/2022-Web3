using Sitecore.LayoutService.Client.Response.Model.Fields;

namespace ExampleApp.RenderingHost.Models
{
    public class ContentBlockModel
    {
        public TextField Title { get; set; }

        public RichTextField Text { get; set; }
    }
}
