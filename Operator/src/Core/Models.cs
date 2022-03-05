using Newtonsoft.Json;

namespace Core
{
    public record InstanceOptions(string InstanceName, string SitecoreAdminPassword, string? User, int MemoryMB, long CPUCount);
    public record StartedResult(string Url, string State);
    public record InstanceDetails(string InstanceName, string HostName, string Url, string ImageName, string State, string Status);
}
