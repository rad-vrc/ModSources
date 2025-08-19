using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Graphics.Shaders;

namespace Terraria.Graphics.Effects
{
	// Token: 0x02000474 RID: 1140
	public class SimpleOverlay : Overlay
	{
		// Token: 0x0600374B RID: 14155 RVA: 0x00587392 File Offset: 0x00585592
		public SimpleOverlay(string textureName, ScreenShaderData shader, EffectPriority priority = EffectPriority.VeryLow, RenderLayers layer = RenderLayers.All) : base(priority, layer)
		{
			this._texture = Main.Assets.Request<Texture2D>((textureName == null) ? "" : textureName);
			this._shader = shader;
		}

		// Token: 0x0600374C RID: 14156 RVA: 0x005873CC File Offset: 0x005855CC
		public SimpleOverlay(string textureName, string shaderName = "Default", EffectPriority priority = EffectPriority.VeryLow, RenderLayers layer = RenderLayers.All) : base(priority, layer)
		{
			this._texture = Main.Assets.Request<Texture2D>((textureName == null) ? "" : textureName);
			this._shader = new ScreenShaderData(Main.ScreenShaderRef, shaderName);
		}

		// Token: 0x0600374D RID: 14157 RVA: 0x00587419 File Offset: 0x00585619
		public ScreenShaderData GetShader()
		{
			return this._shader;
		}

		// Token: 0x0600374E RID: 14158 RVA: 0x00587424 File Offset: 0x00585624
		public override void Draw(SpriteBatch spriteBatch)
		{
			this._shader.UseGlobalOpacity(this.Opacity);
			this._shader.UseTargetPosition(this.TargetPosition);
			this._shader.Apply();
			spriteBatch.Draw(this._texture.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Main.ColorOfTheSkies);
		}

		// Token: 0x0600374F RID: 14159 RVA: 0x00587487 File Offset: 0x00585687
		public override void Update(GameTime gameTime)
		{
			this._shader.Update(gameTime);
		}

		// Token: 0x06003750 RID: 14160 RVA: 0x00587495 File Offset: 0x00585695
		public override void Activate(Vector2 position, params object[] args)
		{
			this.TargetPosition = position;
			this.Mode = OverlayMode.FadeIn;
		}

		// Token: 0x06003751 RID: 14161 RVA: 0x005874A5 File Offset: 0x005856A5
		public override void Deactivate(params object[] args)
		{
			this.Mode = OverlayMode.FadeOut;
		}

		// Token: 0x06003752 RID: 14162 RVA: 0x005874AE File Offset: 0x005856AE
		public override bool IsVisible()
		{
			return this._shader.CombinedOpacity > 0f;
		}

		// Token: 0x0400510B RID: 20747
		private Asset<Texture2D> _texture;

		// Token: 0x0400510C RID: 20748
		private ScreenShaderData _shader;

		// Token: 0x0400510D RID: 20749
		public Vector2 TargetPosition = Vector2.Zero;
	}
}
