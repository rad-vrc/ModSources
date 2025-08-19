using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent
{
	// Token: 0x020001D5 RID: 469
	public abstract class AnOutlinedDrawRenderTargetContent : ARenderTargetContentByRequest
	{
		// Token: 0x06001C18 RID: 7192 RVA: 0x004F157A File Offset: 0x004EF77A
		public void UseColor(Color color)
		{
			this._borderColor = color;
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x004F1584 File Offset: 0x004EF784
		protected override void HandleUseReqest(GraphicsDevice device, SpriteBatch spriteBatch)
		{
			Effect pixelShader = Main.pixelShader;
			if (this._coloringShader == null)
			{
				this._coloringShader = pixelShader.CurrentTechnique.Passes["ColorOnly"];
			}
			new Rectangle(0, 0, this.width, this.height);
			base.PrepareARenderTarget_AndListenToEvents(ref this._target, device, this.width, this.height, RenderTargetUsage.PreserveContents);
			base.PrepareARenderTarget_WithoutListeningToEvents(ref this._helperTarget, device, this.width, this.height, RenderTargetUsage.DiscardContents);
			device.SetRenderTarget(this._helperTarget);
			device.Clear(Color.Transparent);
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null);
			this.DrawTheContent(spriteBatch);
			spriteBatch.End();
			device.SetRenderTarget(this._target);
			device.Clear(Color.Transparent);
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null);
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

		// Token: 0x06001C1A RID: 7194
		internal abstract void DrawTheContent(SpriteBatch spriteBatch);

		// Token: 0x04004367 RID: 17255
		protected int width = 84;

		// Token: 0x04004368 RID: 17256
		protected int height = 84;

		// Token: 0x04004369 RID: 17257
		public Color _borderColor = Color.White;

		// Token: 0x0400436A RID: 17258
		private EffectPass _coloringShader;

		// Token: 0x0400436B RID: 17259
		private RenderTarget2D _helperTarget;
	}
}
