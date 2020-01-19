using Kindred.Base.ECS.Components;
using Kindred.Base.Utils;
using Kindred.Base.Utils.Input;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kindred.Base.ECS.Systems.UpdateSystems
{
    public class PlayerControllerSystem : EntityUpdateSystem
    {
        ComponentMapper<PlayerControllerComponent> _player;
        ComponentMapper<TransformComponent> _transform;
        ComponentMapper<RigidBody> _rigidBody;

        public PlayerControllerSystem()
            : base(Aspect.All(typeof(PlayerControllerComponent)))
        {

        }
        public override void Initialize(IComponentMapperService mapperService)
        {
            _player = mapperService.GetMapper<PlayerControllerComponent>();
            _transform = mapperService.GetMapper<TransformComponent>();
            _rigidBody = mapperService.GetMapper<RigidBody>();
            Logger.WriteLine(WarningLevel.Urgent, "PlayerControllerSystem Initialize Method Not Implemented Yet");
            
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entity in ActiveEntities)
            {

                var player = _player.Get(entity);
                var transform = _transform.Get(entity);
                var rigidBody = _rigidBody.Get(entity);
                Vector2 move = Vector2.Zero;
                move.X = KeyboardInput.GetAxis("Horizontal");
                move.Y = KeyboardInput.GetAxis("Vertical");
                if (move != Vector2.Zero)
                    move = Vector2.Normalize(move);
                rigidBody.Velocity = move;
                //Logger.WriteLine(WarningLevel.Medium, rigidBody.Velocity.ToString());

            }
        }
    }
}
