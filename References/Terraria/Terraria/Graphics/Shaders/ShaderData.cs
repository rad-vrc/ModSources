using System;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Shaders
{
	// Token: 0x02000105 RID: 261
	public class ShaderData
	{
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06001694 RID: 5780 RVA: 0x004C92D3 File Offset: 0x004C74D3
		public Effect Shader
		{
			get
			{
				if (this._shader != null)
				{
					return this._shader.Value;
				}
				return null;
			}
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x004C92EA File Offset: 0x004C74EA
		public ShaderData(Ref<Effect> shader, string passName)
		{
			this._passName = passName;
			this._shader = shader;
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x004C9300 File Offset: 0x004C7500
		public void SwapProgram(string passName)
		{
			this._passName = passName;
			if (passName != null)
			{
				this._effectPass = this.Shader.CurrentTechnique.Passes[passName];
			}
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x004C9328 File Offset: 0x004C7528
		public virtual void Apply()
		{
			if (this._shader != null && this.Shader != null && this._passName != null)
			{
				this._effectPass = this.Shader.CurrentTechnique.Passes[this._passName];
			}
			this._effectPass.Apply();
		}

		// Token: 0x0400136B RID: 4971
		private readonly Ref<Effect> _shader;

		// Token: 0x0400136C RID: 4972
		private string _passName;

		// Token: 0x0400136D RID: 4973
		private EffectPass _effectPass;
	}
}
