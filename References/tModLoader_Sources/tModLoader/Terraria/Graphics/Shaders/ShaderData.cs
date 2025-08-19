using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.Graphics.Shaders
{
	// Token: 0x0200044B RID: 1099
	public class ShaderData
	{
		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06003650 RID: 13904 RVA: 0x0057B80B File Offset: 0x00579A0B
		public Effect Shader
		{
			get
			{
				if (this._shader != null)
				{
					return this._shader.Value;
				}
				if (this._asset != null)
				{
					return this._asset.Value;
				}
				return null;
			}
		}

		// Token: 0x06003651 RID: 13905 RVA: 0x0057B836 File Offset: 0x00579A36
		[Obsolete("Removed in 1.4.5. Use Asset<Effect> version instead. Asset version works with AsyncLoad", true)]
		public ShaderData(Ref<Effect> shader, string passName)
		{
			this._passName = passName;
			this._shader = shader;
		}

		// Token: 0x06003652 RID: 13906 RVA: 0x0057B84C File Offset: 0x00579A4C
		public ShaderData(Asset<Effect> shader, string passName)
		{
			this._passName = passName;
			this._asset = shader;
		}

		// Token: 0x06003653 RID: 13907 RVA: 0x0057B862 File Offset: 0x00579A62
		public void SwapProgram(string passName)
		{
			this._passName = passName;
			if (passName != null)
			{
				this._effectPass = this.Shader.CurrentTechnique.Passes[passName];
			}
		}

		// Token: 0x06003654 RID: 13908 RVA: 0x0057B88A File Offset: 0x00579A8A
		public virtual void Apply()
		{
			if (this.Shader != null && this._passName != null)
			{
				this._effectPass = this.Shader.CurrentTechnique.Passes[this._passName];
			}
			this._effectPass.Apply();
		}

		// Token: 0x04005027 RID: 20519
		private readonly Ref<Effect> _shader;

		// Token: 0x04005028 RID: 20520
		private string _passName;

		// Token: 0x04005029 RID: 20521
		private EffectPass _effectPass;

		// Token: 0x0400502A RID: 20522
		private readonly Asset<Effect> _asset;
	}
}
