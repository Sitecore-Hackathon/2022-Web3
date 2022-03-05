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

            app.MapPost("/start", (string instanceName, string sitecoreAdminPassword, string? userName, int? memoryMB, long? cpuCount, IOperatorEngine engine) =>
            {
                return engine.StartInstance(new InstanceOptions(
                    instanceName, 
                    sitecoreAdminPassword, 
                    userName,
                    memoryMB ?? 1024,
                    cpuCount ?? 2
                    ));
            }).WithName("InstanceStart");

            app.MapPost("/stop", (string instanceName, IOperatorEngine engine) =>
            {
                return engine.StopInstance(new InstanceOptions(instanceName, string.Empty, null, 0, 0));
            }).WithName("InstanceStop");

            app.MapGet("/logs/{instanceName}", async (string instanceName, HttpResponse response, CancellationToken token, IOperatorEngine engine) =>
            {
                var options = new InstanceOptions(instanceName, string.Empty, null, 0, 0);
                return await engine.InstanceLogs(options, response.BodyWriter, token);
            }).WithName("InstanceLogs");

            app.MapGet("/list", (IOperatorEngine engine) =>
            {
                return engine.List();
            }).WithName("ListInstances");
        }
    }
}
