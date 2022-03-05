using DadJokeCli.Commands;
using DadJokeCli.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sitecore.Devex.Client.Cli.Extensibility;
using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using System;
using System.Collections.Generic;

namespace DadJokeCli
{
    public class RegisterExtension : ISitecoreCliExtension
    {
        public IEnumerable<ISubcommand> AddCommands(IServiceProvider container)
        {
            var jokesCommand = new JokesCommand("jokes", "Has all the funny aspects of a joker");

            jokesCommand.AddCommand(container.GetRequiredService<TellAJokeCommand>());

            return new[]
            {
                jokesCommand
            };
        }
        
        public void AddConfiguration(IConfigurationBuilder configBuilder)
        {
        }

        public void AddServices(IServiceCollection serviceCollection)
        {

            serviceCollection
                .AddSingleton(sp => sp.GetService<ILoggerFactory>().CreateLogger<TellAJokeTask>())
                .AddSingleton<TellAJokeCommand>();
        }
    }
}
