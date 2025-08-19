using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent
{
	// Token: 0x020001DA RID: 474
	public class PlayerTitaniumStormBuffTextureContent : ARenderTargetContentByRequest
	{
		// Token: 0x06001C26 RID: 7206 RVA: 0x004F1A63 File Offset: 0x004EFC63
		public PlayerTitaniumStormBuffTextureContent()
		{
			this._shaderData = new MiscShaderData(Main.PixelShaderRef, "TitaniumStorm");
			this._shaderData.UseImage1("Images/Extra_" + 156);
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x004F1AA0 File Offset: 0x004EFCA0
		protected override void HandleUseReqest(GraphicsDevice device, SpriteBatch spriteBatch)
		{
			Main.instance.LoadProjectile(908);
			Asset<Texture2D> asset = TextureAssets.Projectile[908];
			this.UpdateSettingsForRendering(0.6f, 0f, Main.GlobalTimeWrappedHourly, 0.3f);
			base.PrepareARenderTarget_AndListenToEvents(ref this._target, device, asset.Width(), asset.Height(), RenderTargetUsage.PreserveContents);
			device.SetRenderTarget(this._target);
			device.Clear(Color.Transparent);
			DrawData value = new DrawData(asset.Value, Vector2.Zero, Color.White);
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
			this._shaderData.Apply(new DrawData?(value));
			value.Draw(spriteBatch);
			spriteBatch.End();
			device.SetRenderTarget(null);
			this._wasPrepared = true;
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x004F1B63 File Offset: 0x004EFD63
		public void UpdateSettingsForRendering(float gradientContributionFromOriginalTexture, float gradientScrollingSpeed, float flatGradientOffset, float gradientColorDominance)
		{
			this._shaderData.UseColor(gradientScrollingSpeed, gradientContributionFromOriginalTexture, gradientColorDominance);
			this._shaderData.UseOpacity(flatGradientOffset);
		}

		// Token: 0x04004371 RID: 17265
		private MiscShaderData _shaderData;
	}
}
