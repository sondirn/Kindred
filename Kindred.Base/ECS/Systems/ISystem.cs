using System;
using System.Collections.Generic;
using System.Text;

namespace Kindred.Base.ECS.Systems
{
    public interface ISystem : IDisposable
    {
        void Initialize(World world);
    }
}
