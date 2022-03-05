using Docker.DotNet;
using Microsoft.Extensions.DependencyInjection;

namespace Web3.Operator.Engines.DockerEngine
{
    public static class ServicesRegistration
    {
        public static void AddDockerEngineOperator(this IServiceCollection services)
        {
            services.AddTransient<DockerEngineOperator>();

            var url = Environment.GetEnvironmentVariable("ENGINE_URL") ?? "npipe://./pipe/docker_engine";
            services.AddTransient(_ => new DockerClientConfiguration(new Uri(url)));
        }
    }
}
