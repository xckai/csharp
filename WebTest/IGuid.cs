using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTest
{
    public interface IGuid
    {
        Guid GetGuid { get; }
    }
    public interface IScopedService : IGuid
    {

    }
    public interface ISingletonService : IGuid
    {

    }
    public interface ITransientService : IGuid
    {

    }
}
