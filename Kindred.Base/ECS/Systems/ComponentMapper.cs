using MonoGame.Extended.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kindred.Base.ECS.Systems
{
    public abstract class ComponentMapper
    {
        public int Id { get; }
        public Type ComponentType { get; }
        public abstract bool Has(int entityID);
        public abstract void Delete(int entityId);

        protected ComponentMapper(int id, Type componentType)
        {
            Id = id;
            ComponentType = componentType;
        }

    }

    public class ComponentMapper<T> : ComponentMapper 
        where T: class
    {
        public Bag<T> Components { get; }
        private readonly Action<int> _onCompositionChanged;
        public ComponentMapper(int id, Action<int> onCompositionChanged)
            : base(id, typeof(T))
        {
            _onCompositionChanged = onCompositionChanged;
            Components = new Bag<T>();
        }

        public void Put(int entityId, T component)
        {
            Components[entityId] = component;
            _onCompositionChanged(entityId);
        }

        public T Get(Entity entity)
        {
            return Get(entity.Id);
        }


    }
}
