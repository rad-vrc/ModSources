using System;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent
{
	// Token: 0x0200048C RID: 1164
	public abstract class ARenderTargetContentByRequest : INeedRenderTargetContent
	{
		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x060038C2 RID: 14530 RVA: 0x0059468A File Offset: 0x0059288A
		public bool IsReady
		{
			get
			{
				return this._wasPrepared;
			}
		}

		// Token: 0x060038C3 RID: 14531 RVA: 0x00594692 File Offset: 0x00592892
		public void Request()
		{
			this._wasRequested = true;
		}

		// Token: 0x060038C4 RID: 14532 RVA: 0x0059469B File Offset: 0x0059289B
		public RenderTarget2D GetTarget()
		{
			return this._target;
		}

		// Token: 0x060038C5 RID: 14533 RVA: 0x005946A3 File Offset: 0x005928A3
		public void PrepareRenderTarget(GraphicsDevice device, SpriteBatch spriteBatch)
		{
			this._wasPrepared = false;
			if (this._wasRequested)
			{
				this._wasRequested = false;
				this.HandleUseReqest(device, spriteBatch);
			}
		}

		// Token: 0x060038C6 RID: 14534
		protected abstract void HandleUseReqest(GraphicsDevice device, SpriteBatch spriteBatch);

		// Token: 0x060038C7 RID: 14535 RVA: 0x005946C4 File Offset: 0x005928C4
		protected void PrepareARenderTarget_AndListenToEvents(ref RenderTarget2D target, GraphicsDevice device, int neededWidth, int neededHeight, RenderTargetUsage usage)
		{
			if (target == null || target.IsDisposed || target.Width != neededWidth || target.Height != neededHeight)
			{
				if (target != null)
				{
					target.ContentLost -= this.target_ContentLost;
					target.Disposing -= this.target_Disposing;
				}
				target = new RenderTarget2D(device, neededWidth, neededHeight, false, device.PresentationParameters.BackBufferFormat, 0, 0, usage);
				target.ContentLost += this.target_ContentLost;
				target.Disposing += this.target_Disposing;
			}
		}

		// Token: 0x060038C8 RID: 14536 RVA: 0x0059475E File Offset: 0x0059295E
		private void target_Disposing(object sender, EventArgs e)
		{
			this._wasPrepared = false;
			this._target = null;
		}

		// Token: 0x060038C9 RID: 14537 RVA: 0x0059476E File Offset: 0x0059296E
		private void target_ContentLost(object sender, EventArgs e)
		{
			this._wasPrepared = false;
		}

		// Token: 0x060038CA RID: 14538 RVA: 0x00594777 File Offset: 0x00592977
		public void Reset()
		{
			this._wasPrepared = false;
			this._wasRequested = false;
			this._target = null;
		}

		// Token: 0x060038CB RID: 14539 RVA: 0x00594790 File Offset: 0x00592990
		protected void PrepareARenderTarget_WithoutListeningToEvents(ref RenderTarget2D target, GraphicsDevice device, int neededWidth, int neededHeight, RenderTargetUsage usage)
		{
			if (target == null || target.IsDisposed || target.Width != neededWidth || target.Height != neededHeight)
			{
				target = new RenderTarget2D(device, neededWidth, neededHeight, false, device.PresentationParameters.BackBufferFormat, 0, 0, usage);
			}
		}

		// Token: 0x04005209 RID: 21001
		protected RenderTarget2D _target;

		// Token: 0x0400520A RID: 21002
		protected bool _wasPrepared;

		// Token: 0x0400520B RID: 21003
		private bool _wasRequested;
	}
}
