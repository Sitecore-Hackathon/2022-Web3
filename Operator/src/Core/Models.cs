using Newtonsoft.Json;

namespace Core
{
    public record InstanceOptions(string InstanceName, string SitecoreAdminPassword, string? User);
    
    public record InstanceDetails(string InstanceName, string hostName, string ImageName, string State, string Status);
}
