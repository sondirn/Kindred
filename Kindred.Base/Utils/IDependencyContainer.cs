using System;
using System.Collections.Generic;
using System.Text;

namespace Kindred.Base.Utils
{
    public interface IDependencyContainer
    {
        void Register<T>(T dependency);
        T Get<T>();
    }
}
