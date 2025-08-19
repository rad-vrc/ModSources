using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.Graphics.Effects
{
	// Token: 0x0200046A RID: 1130
	public class Filter : GameEffect
	{
		// Token: 0x06003722 RID: 14114 RVA: 0x00586928 File Offset: 0x00584B28
		public Filter(ScreenShaderData shader, EffectPriority priority = EffectPriority.VeryLow)
		{
			this._shader = shader;
			this._priority = priority;
		}

		// Token: 0x06003723 RID: 14115 RVA: 0x0058693E File Offset: 0x00584B3E
		public void Update(GameTime gameTime)
		{
			this._shader.UseGlobalOpacity(this.Opacity);
			this._shader.Update(gameTime);
		}

		// Token: 0x06003724 RID: 14116 RVA: 0x0058695E File Offset: 0x00584B5E
		public void Apply()
		{
			this._shader.Apply();
		}

		// Token: 0x06003725 RID: 14117 RVA: 0x0058696B File Offset: 0x00584B6B
		public ScreenShaderData GetShader()
		{
			return this._shader;
		}

		// Token: 0x06003726 RID: 14118 RVA: 0x00586973 File Offset: 0x00584B73
		public override void Activate(Vector2 position, params object[] args)
		{
			this._shader.UseGlobalOpacity(this.Opacity);
			this._shader.UseTargetPosition(position);
			this.Active = true;
		}

		// Token: 0x06003727 RID: 14119 RVA: 0x0058699B File Offset: 0x00584B9B
		public override void Deactivate(params object[] args)
		{
			this.Active = false;
		}

		// Token: 0x06003728 RID: 14120 RVA: 0x005869A4 File Offset: 0x00584BA4
		public bool IsInUse()
		{
			return this.Active || this.Opacity > 0f;
		}

		// Token: 0x06003729 RID: 14121 RVA: 0x005869BD File Offset: 0x00584BBD
		public bool IsActive()
		{
			return this.Active;
		}

		// Token: 0x0600372A RID: 14122 RVA: 0x005869C5 File Offset: 0x00584BC5
		public override bool IsVisible()
		{
			return this.GetShader().CombinedOpacity > 0f && !this.IsHidden;
		}

		// Token: 0x040050E6 RID: 20710
		public bool Active;

		// Token: 0x040050E7 RID: 20711
		private ScreenShaderData _shader;

		// Token: 0x040050E8 RID: 20712
		public bool IsHidden;
	}
}
