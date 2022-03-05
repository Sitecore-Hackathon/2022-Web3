using System.CommandLine;

namespace Web3.Operator.Cli
{
    internal static class ArgOptions
    {
        internal static readonly Option<bool> SitecoreJoke = new Option<bool>(new[] { "--sitecore-joke", "--sj" }, () => false, "Get a curated Sitecore joke instead of random jokes.");
        internal static readonly Option<string> SitecoreAdminPassword = new Option<string>(new[] {"--sitecore-admin-password", "--pwd"}, "The Sitecore admin password for the instance");
        internal static readonly Option<string> InstanceName = new Option<string>(new[] {"--instance-name", "-n"}, "The instance you wish to stop");
    }
}
