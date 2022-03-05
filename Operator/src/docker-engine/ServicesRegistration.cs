using Docker.DotNet;
using Microsoft.Extensions.DependencyInjection;

namespace Web3.Operator.Engines.DockerEngine
{
    public static class ServicesRegistration
    {
        public static void AddDockerEngineOperator(this IServiceCollection services)
        {
            services.AddTransient<DockerEngineOperator>();

            if (Uri.TryCreate(Environment.GetEnvironmentVariable("ENGINE_URL"), UriKind.Absolute, out var uri))
            {
                services.AddTransient(_ => new DockerClientConfiguration(uri));
            }
        }
    }
}
