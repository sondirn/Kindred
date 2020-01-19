using Comora;
using Kindred.Base.ECS.Components;
using Kindred.Base.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kindred.Base.ECS.Systems.DrawSystems
{
    public class LightsSystem : EntityDrawSystem
    {
        private readonly GraphicsDevice gd;
        private readonly SpriteBatch spriteBatch;
        private ComponentMapper<LightComponent> _lightMapper;
        private ComponentMapper<TransformComponent> _position2DMapper;

        //TestVaiables
        
        
        public LightsSystem(GraphicsDevice graphicsDevice)
            : base(Aspect.All(typeof(LightComponent), typeof(TransformComponent)))
        {
            gd = graphicsDevice;
            spriteBatch = new SpriteBatch(gd);

        }
        public override void Draw(GameTime gameTime)
        {
            Dependencies.GetSB().Begin(Dependencies.GetCamera().Camera, SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearClamp);
            foreach (var entity in ActiveEntities)
            {
                
                var light = _lightMapper.Get(entity);
                var position = _position2DMapper.Get(entity);
                var e = Assets.GetEffect(EffectType.PointLight);
                e.Parameters["inputColor"].SetValue(light.Color);
                e.Parameters["inputIntensity"].SetValue(light.Intensity);
                e.Parameters["innerRadius"].SetValue(light.InnerRadius);
                e.Parameters["innerIntensity"].SetValue(light.InnerIntensity);
                //e.Parameters["bayerMask"].SetValue(mask);
                e.CurrentTechnique.Passes[0].Apply();
                Dependencies.GetSB().FillRectangle(new RectangleF(position.Position.X - (light.Radius / 2), position.Position.Y - (light.Radius / 2), light.Radius, light.Radius), Color.White);
                //Console.WriteLine(light.Position);

            }
            Dependencies.GetSB().End();
            gd.SetRenderTarget(Assets.GetRenderTarget("MainTarget"));
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _lightMapper = mapperService.GetMapper<LightComponent>();
            _position2DMapper = mapperService.GetMapper<TransformComponent>();
        }
    }
}
