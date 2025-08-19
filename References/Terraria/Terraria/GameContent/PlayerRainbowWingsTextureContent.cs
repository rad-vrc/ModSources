using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent
{
	// Token: 0x020001D8 RID: 472
	public class PlayerRainbowWingsTextureContent : ARenderTargetContentByRequest
	{
		// Token: 0x06001C22 RID: 7202 RVA: 0x004F1914 File Offset: 0x004EFB14
		protected override void HandleUseReqest(GraphicsDevice device, SpriteBatch spriteBatch)
		{
			Asset<Texture2D> asset = TextureAssets.Extra[171];
			base.PrepareARenderTarget_AndListenToEvents(ref this._target, device, asset.Width(), asset.Height(), RenderTargetUsage.PreserveContents);
			device.SetRenderTarget(this._target);
			device.Clear(Color.Transparent);
			DrawData value = new DrawData(asset.Value, Vector2.Zero, Color.White);
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
			GameShaders.Misc["HallowBoss"].Apply(new DrawData?(value));
			value.Draw(spriteBatch);
			spriteBatch.End();
			device.SetRenderTarget(null);
			this._wasPrepared = true;
		}
	}
}
