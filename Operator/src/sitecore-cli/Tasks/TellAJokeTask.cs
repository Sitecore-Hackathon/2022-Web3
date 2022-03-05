using DadJokeCli.Clients.ICanHazDadJokeProvider;
using DadJokeCli.Commands;
using Microsoft.Extensions.Logging;
using Sitecore.DevEx.Client.Logging;
using System;
using System.Threading.Tasks;

namespace DadJokeCli.Tasks
{
    public class TellAJokeTask
    {
        private ILogger<TellAJokeTask> _logger;

        public TellAJokeTask(ILogger<TellAJokeTask> logger)
        {
            _logger = logger;
        }
        public async Task Execute(TellAJokeArgs args)
        {
            if (args.SitecoreJoke)
            {
                //I have a joke about Sitecore, but I'm still waiting for it to load. @jammykam
                //I have a Sitecore joke, but it’s not been published yet. @MobeenAnwar
                //I have a joke about sitecore, but it’s not in my version. And it’s 30k in license cost to upgrade. :) @ChristopherAuer
                //I'd send you a joke about EXM but you might not get it. @handee_andee
                //I have a joke about being an MVP, but it's under NDA so I can't tell anyone... @kenkre_rohan
                ColorLogExtensions.LogConsole(_logger, LogLevel.Error, "I have a joke about why are Sitecore Developers are so forgetful? Unfortunately I don't remember it because Sitecore is using all my memory");
                return;
            }

            ICanHazDadJokeClient client = new ICanHazDadJokeClient("FxxK PxTxI!");
            var joke = await client.GetRandomJokeAsync();
            ColorLogExtensions.LogConsole(_logger, LogLevel.Warning, joke.DadJoke);
        }
    }
}