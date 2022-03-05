﻿using Sitecore.DevEx.Client.Tasks;

namespace Web3.Operator.Cli.Commands
{
    public class StopInstanceArgs : TaskOptionsBase
    {
        public string InstanceName { get; set; }

        public override void Validate()
        {
            Require(nameof(InstanceName));
        }
    }
}