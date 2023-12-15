using System;
using System.Collections.Generic;

namespace MoleQ.Settings;

public interface IServiceSettings
{
    void ApplyToServices(IDictionary<Type, object> services);
    void ExtractFromServices(IDictionary<Type, object> services);
}