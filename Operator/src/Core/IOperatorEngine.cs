﻿using System.IO.Pipelines;

namespace Core
{
    public interface IOperatorEngine
    {
        Task<string> StartInstance(InstanceOptions options);
        Task StopInstance(InstanceOptions options);
        Task<ICollection<InstanceDetails>> List();
        Task<IAsyncEnumerable<byte>> InstanceLogs(InstanceOptions options, PipeWriter outputStream, CancellationToken token);
    }
}
