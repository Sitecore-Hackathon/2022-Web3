using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sitecore.Devex.Client.Cli.Extensibility;
using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using System;
using System.Collections.Generic;
using Web3.Operator.Cli.Clients;
using Web3.Operator.Cli.Commands;
using Web3.Operator.Cli.Services;
using Web3.Operator.Cli.Tasks;

namespace Web3.Operator.Cli
{
    public class RegisterExtension : ISitecoreCliExtension
    {
        public IEnumerable<ISubcommand> AddCommands(IServiceProvider container)
        {
            var jokeCommand = new JokeCommand("joke", "Your best friend while waiting for Sitecore to start");
            jokeCommand.AddCommand(container.GetRequiredService<TellAJokeCommand>());

            var instanceCommand = new InstanceCommand("instance", "Manage temporary single container Sitecore instance");
            instanceCommand.AddCommand(container.GetRequiredService<InitInstanceCommand>());
            instanceCommand.AddCommand(container.GetRequiredService<StartInstanceCommand>());
            instanceCommand.AddCommand(container.GetRequiredService<StopInstanceCommand>());
            instanceCommand.AddCommand(container.GetRequiredService<LogsCommand>());
            instanceCommand.AddCommand(container.GetRequiredService<ListInstancesCommand>());

            var steveCommand = new JokeCommand("steve", "A word from the CEO");
            steveCommand.AddAlias("symposium");
            steveCommand.AddCommand(container.GetRequiredService<SteveSaysCommand>());

            return new ISubcommand[]
            {
                jokeCommand,
                instanceCommand,
                steveCommand
            };
        }
        
        public void AddConfiguration(IConfigurationBuilder configBuilder)
        {
        }

        public void AddServices(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSingleton<CorporateBsGeneratorClient>()
                .AddSingleton<ICanHazDadJokeClient>()
                .AddSingleton<TellAJokeCommand>()
                .AddSingleton<SteveSaysCommand>()
                .AddSingleton<InitInstanceCommand>()
                .AddSingleton<StartInstanceCommand>()
                .AddSingleton<StopInstanceCommand>()
                .AddSingleton<LogsCommand>()
                .AddSingleton<ListInstancesCommand>()
                .AddSingleton<IUserConfigService, UserConfigService>()
                .AddSingleton<IOperatorClient, OperatorClient>()
                .AddSingleton(sp => sp.GetService<ILoggerFactory>().CreateLogger<TellAJokeTask>())
                .AddSingleton(sp => sp.GetService<ILoggerFactory>().CreateLogger<SteveSaysTask>())
                .AddSingleton(sp => sp.GetService<ILoggerFactory>().CreateLogger<InitInstanceTask>())
                .AddSingleton(sp => sp.GetService<ILoggerFactory>().CreateLogger<StartInstanceTask>())
                .AddSingleton(sp => sp.GetService<ILoggerFactory>().CreateLogger<StopInstanceTask>())
                .AddSingleton(sp => sp.GetService<ILoggerFactory>().CreateLogger<LogsTask>())
                .AddSingleton(sp => sp.GetService<ILoggerFactory>().CreateLogger<ListInstancesTask>());
        }
    }
}
