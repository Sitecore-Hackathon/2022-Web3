using Microsoft.Extensions.DependencyInjection;

namespace Web3.Operator.Engines.DockerEngine
{
    public class DockerEngineConfiguration
    {
        public string? Url { get; set; }
    }

    public static class ServicesRegistration
    {
        public static void AddDockerEngineOperator(this IServiceCollection services)
        {
            services.AddTransient<DockerEngineOperator>();
        }
    }
}
