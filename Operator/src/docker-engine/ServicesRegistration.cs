using Docker.DotNet;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace Web3.Operator.Engines.DockerEngine
{
    public class DockerEngineConfiguration
    {
        public string Url { get; set; }
    }

    public static class ServicesRegistration
    {
        public static void AddDockerEngineOperator(this IServiceCollection services)
        {
            services.AddTransient<DockerEngineOperator>();
        }
    }
}
