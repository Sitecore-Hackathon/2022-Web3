namespace Web3.Operator.Engines.Core
{
    public class OperatorConfiguration
    {
        public string InstanceImage { get; set; } = "mcr.microsoft.com/windows/servercore/iis:windowsservercore-ltsc2019";
        public string InstanceNamePattern { get; set; } = "sc_{0}";
        public string TraefikEntrypoint { get; set; } = "web";
        public string HostNamePattern { get; set; } = "{0}.sitecore.localhost";
        public bool HostNameTls { get; set; } = false;
        public int DefaultMemoryMb { get; set; }
        public int DefaultCpu { get; set; }
        public string OperatorNameLabel { get; set; } = "sitecoreoperator.name";
        public string OperatorUserLabel { get; set; } = "sitecoreoperator.user";
        public string DockerNetworkName { get; set; } = "operatornet";

    }
}
