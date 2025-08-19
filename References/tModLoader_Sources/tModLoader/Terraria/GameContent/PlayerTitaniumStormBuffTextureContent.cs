using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent
{
	// Token: 0x020004AD RID: 1197
	public class PlayerTitaniumStormBuffTextureContent : ARenderTargetContentByRequest
	{
		// Token: 0x0600399C RID: 14748 RVA: 0x00599804 File Offset: 0x00597A04
		public PlayerTitaniumStormBuffTextureContent()
		{
			this._shaderData = new MiscShaderData(Main.PixelShaderRef, "TitaniumStorm");
			this._shaderData.UseImage1("Images/Extra_" + 156.ToString());
		}

		// Token: 0x0600399D RID: 14749 RVA: 0x00599850 File Offset: 0x00597A50
		protected override void HandleUseReqest(GraphicsDevice device, SpriteBatch spriteBatch)
		{
			Main.instance.LoadProjectile(908);
			Asset<Texture2D> asset = TextureAssets.Projectile[908];
			this.UpdateSettingsForRendering(0.6f, 0f, Main.GlobalTimeWrappedHourly, 0.3f);
			base.PrepareARenderTarget_AndListenToEvents(ref this._target, device, asset.Width(), asset.Height(), 1);
			device.SetRenderTarget(this._target);
			device.Clear(Color.Transparent);
			DrawData value = new DrawData(asset.Value, Vector2.Zero, Color.White);
			spriteBatch.Begin(1, BlendState.AlphaBlend);
			this._shaderData.Apply(new DrawData?(value));
			value.Draw(spriteBatch);
			spriteBatch.End();
			device.SetRenderTarget(null);
			this._wasPrepared = true;
		}

		// Token: 0x0600399E RID: 14750 RVA: 0x00599913 File Offset: 0x00597B13
		public void UpdateSettingsForRendering(float gradientContributionFromOriginalTexture, float gradientScrollingSpeed, float flatGradientOffset, float gradientColorDominance)
		{
			this._shaderData.UseColor(gradientScrollingSpeed, gradientContributionFromOriginalTexture, gradientColorDominance);
			this._shaderData.UseOpacity(flatGradientOffset);
		}

		// Token: 0x04005270 RID: 21104
		private MiscShaderData _shaderData;
	}
}
