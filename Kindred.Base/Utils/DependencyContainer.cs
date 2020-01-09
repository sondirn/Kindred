using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kindred.Base.Utils
{
    
    class DependencyContainer : IDependencyContainer
    {
        ContentManager contentManager;
        public T Get<T>()
        {
            throw new NotImplementedException();
        }

        public void Register<T>(T dependency)
        {
            //contentManager = dependency;
        }
    }
}
