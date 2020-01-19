using Comora;
using Kindred.Base.ECS.Components;
using Kindred.Base.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kindred.Base.ECS.Systems.DrawSystems
{
    public class SpriteSystem : EntityDrawSystem
    {
        private readonly GraphicsDeviceInformation gd;
        private readonly SpriteBatch spriteBatch;
        private ComponentMapper<SpriteComponent> _sprite;
        private ComponentMapper<TransformComponent> _transform;
        public SpriteSystem(GraphicsDeviceInformation graphicsDevice)
            : base(Aspect.All(typeof(SpriteComponent), typeof(TransformComponent)))
        {

        }
        public override void Draw(GameTime gameTime)
        {
            
            spriteBatch.Begin(Dependencies.GetCamera().Camera, SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearClamp);
            foreach (var entity in ActiveEntities)
            {

            }
            spriteBatch.End();
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _sprite = mapperService.GetMapper<SpriteComponent>();
            _transform = mapperService.GetMapper<TransformComponent>();
        }
    }
}
