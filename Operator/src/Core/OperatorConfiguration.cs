﻿namespace Web3.Operator.Engines.Core
{
    public class OperatorConfiguration
    {
        public string InstanceImage { get; set; } = Environment.GetEnvironmentVariable("INSTANCE_IMAGE") ?? "mcr.microsoft.com/windows/servercore/iis:windowsservercore-ltsc2019";
        public string InstanceNamePattern { get; set; } = "sc_{0}";
        public string TraefikEntrypoint { get; set; } = "websecure";
        public string HostNamePattern { get; set; } = "{0}.sitecore.localhost";
        public bool HostNameTls { get; set; } = false;
    }
}
