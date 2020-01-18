using Comora;
using Kindred.Base.ECS.Components;
using Kindred.Base.Graphics.LightSystem;
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
        private ComponentMapper _lightMapper;
        private ComponentMapper _position2DMapper;
        private Texture2D mask;
        public LightsSystem(GraphicsDevice graphicsDevice)
            : base(Aspect.All(typeof(Light), typeof(Position2D)))
        {
            gd = graphicsDevice;
            spriteBatch = new SpriteBatch(gd);

        }
        public override void Draw(GameTime gameTime)
        {
            Assets.AddTexture(@"Effects\BayerMatrix2048");
            mask = Assets.GetTexture(@"Effects\BayerMatrix2048");

            Dependencies.GetSB().Begin(Dependencies.GetCamera().Camera, SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearClamp);
            foreach (var entity in ActiveEntities)
            {
                var e = Assets.GetEffect(EffectType.PointLight);
                e.Parameters["inputColor"].SetValue(new Vector3(255, 255, 255));
                e.Parameters["inputIntensity"].SetValue(.5f);
                e.Parameters["innerRadius"].SetValue(0f);
                e.Parameters["innerIntensity"].SetValue(0f);
                e.Parameters["bayerMask"].SetValue(mask);
                e.CurrentTechnique.Passes[0].Apply();
                Dependencies.GetSB().FillRectangle(new RectangleF(0, 0, 360, 360), Color.White);
                e.Parameters["inputColor"].SetValue(new Vector3(255, 0, 0));
                e.CurrentTechnique.Passes[0].Apply();
                Dependencies.GetSB().FillRectangle(new RectangleF(50, 50, 360, 360), Color.White);
            }
            Dependencies.GetSB().End();
            gd.SetRenderTarget(Assets.GetRenderTarget("MainTarget"));
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _lightMapper = mapperService.GetMapper<Light>();
            _position2DMapper = mapperService.GetMapper<Position2D>();
        }
    }
}
