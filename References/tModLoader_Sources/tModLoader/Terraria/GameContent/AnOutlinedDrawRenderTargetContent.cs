using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent
{
	// Token: 0x0200048B RID: 1163
	public abstract class AnOutlinedDrawRenderTargetContent : ARenderTargetContentByRequest
	{
		// Token: 0x060038BE RID: 14526 RVA: 0x0059447C File Offset: 0x0059267C
		public void UseColor(Color color)
		{
			this._borderColor = color;
		}

		// Token: 0x060038BF RID: 14527 RVA: 0x00594488 File Offset: 0x00592688
		protected override void HandleUseReqest(GraphicsDevice device, SpriteBatch spriteBatch)
		{
			Effect pixelShader = Main.pixelShader;
			if (this._coloringShader == null)
			{
				this._coloringShader = pixelShader.CurrentTechnique.Passes["ColorOnly"];
			}
			new Rectangle(0, 0, this.width, this.height);
			base.PrepareARenderTarget_AndListenToEvents(ref this._target, device, this.width, this.height, 1);
			base.PrepareARenderTarget_WithoutListeningToEvents(ref this._helperTarget, device, this.width, this.height, 0);
			device.SetRenderTarget(this._helperTarget);
			device.Clear(Color.Transparent);
			spriteBatch.Begin(1, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null);
			this.DrawTheContent(spriteBatch);
			spriteBatch.End();
			device.SetRenderTarget(this._target);
			device.Clear(Color.Transparent);
			spriteBatch.Begin(1, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null);
			this._coloringShader.Apply();
			int num = 2;
			int num2 = num * 2;
			for (int i = -num2; i <= num2; i += num)
			{
				for (int j = -num2; j <= num2; j += num)
				{
					if (Math.Abs(i) + Math.Abs(j) == num2)
					{
						spriteBatch.Draw(this._helperTarget, new Vector2((float)i, (float)j), Color.Black);
					}
				}
			}
			num2 = num;
			for (int k = -num2; k <= num2; k += num)
			{
				for (int l = -num2; l <= num2; l += num)
				{
					if (Math.Abs(k) + Math.Abs(l) == num2)
					{
						spriteBatch.Draw(this._helperTarget, new Vector2((float)k, (float)l), this._borderColor);
					}
				}
			}
			pixelShader.CurrentTechnique.Passes[0].Apply();
			spriteBatch.Draw(this._helperTarget, Vector2.Zero, Color.White);
			spriteBatch.End();
			device.SetRenderTarget(null);
			this._wasPrepared = true;
		}

		// Token: 0x060038C0 RID: 14528
		internal abstract void DrawTheContent(SpriteBatch spriteBatch);

		// Token: 0x04005204 RID: 20996
		protected int width = 84;

		// Token: 0x04005205 RID: 20997
		protected int height = 84;

		// Token: 0x04005206 RID: 20998
		public Color _borderColor = Color.White;

		// Token: 0x04005207 RID: 20999
		private EffectPass _coloringShader;

		// Token: 0x04005208 RID: 21000
		private RenderTarget2D _helperTarget;
	}
}
