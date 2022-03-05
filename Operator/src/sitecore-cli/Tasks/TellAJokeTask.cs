using Microsoft.Extensions.Logging;
using Sitecore.DevEx.Client.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web3.Operator.Cli.Commands;

namespace Web3.Operator.Cli.Tasks
{
    public class TellAJokeTask
    {
        private ILogger<TellAJokeTask> _logger;
        private Dictionary<string, string> sitecoreJokes = new Dictionary<string, string>
        {
            {"I have a joke about why are Sitecore Developers are so forgetful? Unfortunately I don't remember it because Sitecore is using all my memory.", "Steven Singh" },
            {"I have a joke about Sitecore, but I'm still waiting for it to load.", "@jammykam" },
            {"I have a Sitecore joke, but it’s not been published yet.", "@MobeenAnwar" },
            {"I have a joke about sitecore, but it’s not in my version. And it’s 30k in license cost to upgrade. :)", "@ChristopherAuer" },
            {"I'd send you a joke about EXM but you might not get it.", "@handee_andee" },
            {"I have a joke about being an MVP, but it's under NDA so I can't tell anyone... ", "@kenkre_rohan" },
            {"How do you deliver Sitecore into China? Using Containers!", "Steven Singh" }
        };

        public TellAJokeTask(ILogger<TellAJokeTask> logger)
        {
            _logger = logger;
        }
        public async Task Execute(TellAJokeArgs args)
        {
            if (args.SitecoreJoke)
            {
                Random random = new Random();
                var jokeIndex = random.Next(sitecoreJokes.Count);

                var randomJoke = sitecoreJokes.ElementAt(jokeIndex);
                ColorLogExtensions.LogConsole(_logger, LogLevel.Error, GetJokeText(randomJoke.Key, randomJoke.Value));
                return;
            }

            ICanHazDadJokeClient client = new ICanHazDadJokeClient("FxxK PxTxI!");
            var joke = await client.GetRandomJokeAsync();
            ColorLogExtensions.LogConsole(_logger, LogLevel.Warning, GetJokeText(joke.DadJoke, "ICanHazDadJoke"));
        }

        private string GetJokeText(string joke, string author)
        {
            return $"{joke} by {author}";
        }
    }
}