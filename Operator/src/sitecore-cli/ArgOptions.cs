using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace Web3.Operator.Cli
{
    internal static class ArgOptions
    {
        internal static readonly Option<bool> SitecoreJoke = new Option<bool>(new[] { "--sitecore-joke", "--sj" }, () => false, "Get a curated Sitecore joke instead of random jokes.");
    }
}
