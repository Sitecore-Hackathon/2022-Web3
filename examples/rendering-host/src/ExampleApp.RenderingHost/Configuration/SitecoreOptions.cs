using System;

namespace ExampleApp.RenderingHost.Configuration
{
    public class SitecoreOptions
    {
        public static readonly string Key = "Sitecore";

        public Uri InstanceUri { get; set; }
        public string LayoutServicePath { get; set; } = "/sitecore/api/layout/render/jss";
        public string DefaultSiteName { get; set; }
        public string ApiKey { get; set; }
        public Uri RenderingHostUri { get; set; }

        public Uri LayoutServiceUri
        {
            get
            {
                if (InstanceUri == null)
                {
                    return null;
                }

                return new Uri(InstanceUri, LayoutServicePath);
            }
        }
    }
}
