namespace Core
{
    public interface IOperatorEngine
    {
        Task<string> StartInstance(InstanceOptions options);
        Task StopInstance(InstanceOptions options);
        Task<ICollection<InstanceDetails>> List();
    }
}
