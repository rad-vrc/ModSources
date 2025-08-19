using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent
{
	// Token: 0x020004AA RID: 1194
	public class PlayerRainbowWingsTextureContent : ARenderTargetContentByRequest
	{
		// Token: 0x0600398B RID: 14731 RVA: 0x00598728 File Offset: 0x00596928
		protected override void HandleUseReqest(GraphicsDevice device, SpriteBatch spriteBatch)
		{
			Asset<Texture2D> asset = TextureAssets.Extra[171];
			base.PrepareARenderTarget_AndListenToEvents(ref this._target, device, asset.Width(), asset.Height(), 1);
			device.SetRenderTarget(this._target);
			device.Clear(Color.Transparent);
			DrawData value = new DrawData(asset.Value, Vector2.Zero, Color.White);
			spriteBatch.Begin(1, BlendState.AlphaBlend);
			GameShaders.Misc["HallowBoss"].Apply(new DrawData?(value));
			value.Draw(spriteBatch);
			spriteBatch.End();
			device.SetRenderTarget(null);
			this._wasPrepared = true;
		}
	}
}
