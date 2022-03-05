using Core;

namespace Web3.Operator.Api
{
    public static class Routes
    {
        public static void UseOperatorRoutes(this WebApplication app)
        {
            app.MapGet("/", () =>
            {
                return "Hi, I am Sitecore Operator";
            }).ExcludeFromDescription();

            app.MapPost("/start", (string instanceName, string sitecoreAdminPassword, string? userName, IOperatorEngine engine) =>
            {
                return engine.StartInstance(new InstanceOptions(instanceName, sitecoreAdminPassword, userName));
            }).WithName("StartInstance");

            app.MapPost("/stop", (string instanceName, IOperatorEngine engine) =>
            {
                return engine.StopInstance(new InstanceOptions(instanceName, string.Empty, null));
            }).WithName("StopInstance");

            app.MapGet("/list", (IOperatorEngine engine) =>
            {
                return engine.List();
            }).WithName("ListInstances");
        }
    }
}
