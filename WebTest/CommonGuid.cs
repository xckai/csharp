using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTest
{
    class CommonGuid : IScopedService, ISingletonService, ITransientService
    {
        public CommonGuid() : this(Guid.NewGuid())
        {
        }
        public CommonGuid(Guid id)
        {
            GetGuid = id;
        }
        public Guid GetGuid { get; private set; }
    }
}
