using Kindred.Base.ECS.Components;
using Kindred.Base.Utils;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kindred.Base.ECS.Systems.UpdateSystems
{
    public class PhysicsSystem : EntityUpdateSystem
    {
        ComponentMapper<TransformComponent> _transform;
        ComponentMapper<RigidBody> _rigidBody;
        public PhysicsSystem()
            : base(Aspect.All(typeof(TransformComponent), typeof(RigidBody)))
        {
            
        }
        public override void Initialize(IComponentMapperService mapperService)
        {
            _transform = mapperService.GetMapper<TransformComponent>();
            _rigidBody = mapperService.GetMapper<RigidBody>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entity in ActiveEntities)
            {
                var transform = _transform.Get(entity);
                var rigidBody = _rigidBody.Get(entity);

                transform.Position += rigidBody.Velocity;
                
            }
        }
    }
}
