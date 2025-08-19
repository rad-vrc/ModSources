using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.Graphics.Effects
{
	// Token: 0x0200010B RID: 267
	public class Filter : GameEffect
	{
		// Token: 0x060016B7 RID: 5815 RVA: 0x004C97C8 File Offset: 0x004C79C8
		public Filter(ScreenShaderData shader, EffectPriority priority = EffectPriority.VeryLow)
		{
			this._shader = shader;
			this._priority = priority;
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x004C97DE File Offset: 0x004C79DE
		public void Update(GameTime gameTime)
		{
			this._shader.UseGlobalOpacity(this.Opacity);
			this._shader.Update(gameTime);
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x004C97FE File Offset: 0x004C79FE
		public void Apply()
		{
			this._shader.Apply();
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x004C980B File Offset: 0x004C7A0B
		public ScreenShaderData GetShader()
		{
			return this._shader;
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x004C9813 File Offset: 0x004C7A13
		public override void Activate(Vector2 position, params object[] args)
		{
			this._shader.UseGlobalOpacity(this.Opacity);
			this._shader.UseTargetPosition(position);
			this.Active = true;
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x004C983B File Offset: 0x004C7A3B
		public override void Deactivate(params object[] args)
		{
			this.Active = false;
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x004C9844 File Offset: 0x004C7A44
		public bool IsInUse()
		{
			return this.Active || this.Opacity > 0f;
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x004C985D File Offset: 0x004C7A5D
		public bool IsActive()
		{
			return this.Active;
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x004C9865 File Offset: 0x004C7A65
		public override bool IsVisible()
		{
			return this.GetShader().CombinedOpacity > 0f && !this.IsHidden;
		}

		// Token: 0x04001384 RID: 4996
		public bool Active;

		// Token: 0x04001385 RID: 4997
		private ScreenShaderData _shader;

		// Token: 0x04001386 RID: 4998
		public bool IsHidden;
	}
}
