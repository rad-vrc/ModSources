using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Graphics.Shaders;

namespace Terraria.Graphics.Effects
{
	// Token: 0x02000114 RID: 276
	public class SimpleOverlay : Overlay
	{
		// Token: 0x060016DF RID: 5855 RVA: 0x004CA1B7 File Offset: 0x004C83B7
		public SimpleOverlay(string textureName, ScreenShaderData shader, EffectPriority priority = EffectPriority.VeryLow, RenderLayers layer = RenderLayers.All) : base(priority, layer)
		{
			this._texture = Main.Assets.Request<Texture2D>((textureName == null) ? "" : textureName, 1);
			this._shader = shader;
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x004CA1F0 File Offset: 0x004C83F0
		public SimpleOverlay(string textureName, string shaderName = "Default", EffectPriority priority = EffectPriority.VeryLow, RenderLayers layer = RenderLayers.All) : base(priority, layer)
		{
			this._texture = Main.Assets.Request<Texture2D>((textureName == null) ? "" : textureName, 1);
			this._shader = new ScreenShaderData(Main.ScreenShaderRef, shaderName);
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x004CA23E File Offset: 0x004C843E
		public ScreenShaderData GetShader()
		{
			return this._shader;
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x004CA248 File Offset: 0x004C8448
		public override void Draw(SpriteBatch spriteBatch)
		{
			this._shader.UseGlobalOpacity(this.Opacity);
			this._shader.UseTargetPosition(this.TargetPosition);
			this._shader.Apply();
			spriteBatch.Draw(this._texture.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Main.ColorOfTheSkies);
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x004CA2AB File Offset: 0x004C84AB
		public override void Update(GameTime gameTime)
		{
			this._shader.Update(gameTime);
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x004CA2B9 File Offset: 0x004C84B9
		public override void Activate(Vector2 position, params object[] args)
		{
			this.TargetPosition = position;
			this.Mode = OverlayMode.FadeIn;
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x004CA2C9 File Offset: 0x004C84C9
		public override void Deactivate(params object[] args)
		{
			this.Mode = OverlayMode.FadeOut;
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x004CA2D2 File Offset: 0x004C84D2
		public override bool IsVisible()
		{
			return this._shader.CombinedOpacity > 0f;
		}

		// Token: 0x0400139E RID: 5022
		private Asset<Texture2D> _texture;

		// Token: 0x0400139F RID: 5023
		private ScreenShaderData _shader;

		// Token: 0x040013A0 RID: 5024
		public Vector2 TargetPosition = Vector2.Zero;
	}
}
